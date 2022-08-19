using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Security.Application.Dto.Permission;
using Security.Application.Interfaces;
using Security.Application.MainModule.Base;
using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Transversal.Common;
using Security.Transversal.Common.Enum;
using Security.Transversal.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Application.MainModule
{
    public class PermissionAppService : BaseAppService, IPermissionAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMenuRepository _menuRepository;

        public PermissionAppService(IServiceProvider serviceProvider,
            IUserRepository userRepository,
            IMenuRepository menuRepository) : base(serviceProvider)
        {
            _userRepository = userRepository;
            _menuRepository = menuRepository;
        }

        public async Task<Response<PermissionGetResponseDto>> GetByUserAsync()
        {
            var response = TypeMessageHelper.MessageSuccess<PermissionGetResponseDto>();
            #region Obtener Menus y acciones que se tiene acceso
            var userentity = await GetUserEntity();
            var permiso = new PermissionGetResponseDto();
            permiso.user = Mapper.Map<PermisoUserDto>(userentity);
            permiso.user.Roles = Mapper.Map<List<PermisoRolDto>>(userentity.UserRoles);
            this.GetMenus(userentity, permiso);
            #endregion
            //permiso.user.Rol.Menus = listMenu.Where(x => x.State == (int)StateEnum.Active).GroupBy(x => new { x.Id }).Select(menu => menu.First()).ToList();
            //permiso.user.Rol.Menus.ForEach(menu =>
            //{
            //    var action = listMenuAction.Where(x => x.IdMenu == menu.Id && x.State == (int)StateEnum.Active).Select(x => x.Action).ToList();
            //    menu.Actions.AddRange(action);
            //});
            response.Data = permiso;
            return response;
        }

        public async Task<Response<PermissionGetResponseDto>> GetByRoleAsync(int IdRole)
        {
            var response = TypeMessageHelper.MessageSuccess<PermissionGetResponseDto>();
            var userentity = await GetUserEntity();

            var permiso = new PermissionGetResponseDto();
            permiso.user = Mapper.Map<PermisoUserDto>(userentity);
            permiso.user.Roles = Mapper.Map<List<PermisoRolDto>>(userentity.UserRoles.Where(x => x.IdRole == IdRole).ToList());
            this.GetMenus(userentity, permiso);
            response.Data = permiso;
            return response;
        }

        #region Private Methods
        private async Task<User> GetUserEntity()
        {
            return await _userRepository.Find(x => x.Id == 1)//CurrentUser.Id)
                    .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                    .ThenInclude(x => x.MenuRoles)
                    .ThenInclude(x => x.MenuAction)
                    .ThenInclude(x => x.Menu)

                    .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                    .ThenInclude(x => x.MenuRoles)
                    .ThenInclude(x => x.MenuAction)
                    .ThenInclude(x => x.Action)
                    .FirstOrDefaultAsync();
        }

        private void GetMenus(User userentity, PermissionGetResponseDto permiso)
        {
            #region Obtener Todos los menús en general
            var menusEntity = _menuRepository.Find(x => x.State == (int)StateEnum.Active).ToList();
            List<PermisoMenuLevelGenericDto> lstMenusGeneric = Mapper.Map<List<PermisoMenuLevelGenericDto>>(menusEntity);
            lstMenusGeneric = lstMenusGeneric.Where(x => x.State == (int)StateEnum.Active).ToList();

            #region Obtener Menus todos los niveles
            List<PermisoMenuLevel1Dto> menusLevel1 = new List<PermisoMenuLevel1Dto>();
            var level1 = lstMenusGeneric.Where(x => x.Level == 1).ToList();
            menusLevel1 = Mapper.Map<List<PermisoMenuLevel1Dto>>(level1);

            List<PermisoMenuLevel2Dto> menusLevel2 = new List<PermisoMenuLevel2Dto>();
            var level2 = lstMenusGeneric.Where(x => x.Level == 2).ToList();
            menusLevel2 = Mapper.Map<List<PermisoMenuLevel2Dto>>(level2);

            List<PermisoMenuLevel3Dto> menusLevel3 = new List<PermisoMenuLevel3Dto>();
            var level3 = lstMenusGeneric.Where(x => x.Level == 3).ToList();
            menusLevel3 = Mapper.Map<List<PermisoMenuLevel3Dto>>(level3);

            List<PermisoMenuLevel4Dto> menusLevel4 = new List<PermisoMenuLevel4Dto>();
            var level4 = lstMenusGeneric.Where(x => x.Level == 3).ToList();
            menusLevel4 = Mapper.Map<List<PermisoMenuLevel4Dto>>(level4);
            #endregion

            #endregion

            List<PermisoMenuDto> listMenu = new List<PermisoMenuDto>();
            List<PermisoMenuActionDto> listMenuAction = new List<PermisoMenuActionDto>();

            foreach (var rol in permiso.user.Roles)
            {
                var menurole = userentity.UserRoles.Where(x => x.IdRole == rol.Id).FirstOrDefault().Role.MenuRoles;
                if (menurole.Any())
                {
                    rol.MenusRole = Mapper.Map<List<PermisoMenuRoleDto>>(menurole);
                    foreach (var menuRole in rol.MenusRole)
                    {
                        var menuAction = menurole.FirstOrDefault(x => x.Id == menuRole.Id && x.State == (int)StateEnum.Active).MenuAction;
                        if (menuAction is not null)
                        {
                            menuRole.MenuAction = Mapper.Map<PermisoMenuActionDto>(menuAction);
                            menuRole.MenuAction.Menu = Mapper.Map<PermisoMenuDto>(menuAction.Menu);
                            menuRole.MenuAction.Action = Mapper.Map<PermisoActionDto>(menuAction.Action);
                            menuRole.MenuAction.IdRol = rol.Id;
                            menuRole.IdRole = rol.Id;
                            menuRole.IdMenu = menuRole.MenuAction.IdMenu;
                            listMenuAction.Add(menuRole.MenuAction);
                            listMenu.Add(menuRole.MenuAction.Menu);
                        }
                    }

                    #region Setear Menus Anidados
                    List<int> IdMenusAcceso = rol.MenusRole.Select(x => x.IdMenu).Distinct().ToList();
                    rol.Menus = Mapper.Map<List<PermisoMenuLevel1Dto>>(menusLevel1.Where(x => IdMenusAcceso.Contains(x.Id)).ToList());
                    if (rol.Menus.Any())
                    {
                        foreach (var menuLevel1 in rol.Menus)
                        {
                            menuLevel1.Actions = new List<PermisoActionDto>();
                            menuLevel1.Actions.AddRange(listMenuAction.Where(x => x.IdMenu == menuLevel1.Id && x.IdRol == rol.Id).Select(x => x.Action).ToList());

                            var MisMenusLevel2 = menusLevel2.Where(x => x.IdParent == menuLevel1.Id && IdMenusAcceso.Contains(x.Id)).ToList();
                            menuLevel1.Menus = Mapper.Map<List<PermisoMenuLevel2Dto>>(MisMenusLevel2);
                            if (menuLevel1.Menus.Any())
                            {
                                foreach (var menuLevel2 in menuLevel1.Menus)
                                {
                                    menuLevel2.Actions = new List<PermisoActionDto>();
                                    menuLevel2.Actions.AddRange(listMenuAction.Where(x => x.IdMenu == menuLevel2.Id && x.IdRol == rol.Id).Select(x => x.Action).ToList());

                                    var MisMenusLevel3 = menusLevel3.Where(x => x.IdParent == menuLevel2.Id && IdMenusAcceso.Contains(x.Id)).ToList();
                                    menuLevel2.Menus = Mapper.Map<List<PermisoMenuLevel3Dto>>(MisMenusLevel3);
                                    if (menuLevel2.Menus.Any())
                                    {
                                        foreach (var menuLevel3 in menuLevel2.Menus)
                                        {
                                            menuLevel3.Actions = new List<PermisoActionDto>();
                                            menuLevel3.Actions.AddRange(listMenuAction.Where(x => x.IdMenu == menuLevel3.Id && x.IdRol == rol.Id).Select(x => x.Action).ToList());

                                            var MisMenusLevel4 = menusLevel4.Where(x => x.IdParent == menuLevel3.Id && IdMenusAcceso.Contains(x.Id)).ToList();
                                            menuLevel3.Menus = Mapper.Map<List<PermisoMenuLevel4Dto>>(MisMenusLevel4);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
        }
        #endregion

    }
}
