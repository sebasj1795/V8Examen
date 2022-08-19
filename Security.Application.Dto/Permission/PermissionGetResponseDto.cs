using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Security.Application.Dto.Permission
{
    public class PermissionGetResponseDto
    {
        public PermissionGetResponseDto()
        {
            user = new PermisoUserDto();
        }
        public PermisoUserDto user { get; set; }

    }

    public class PermisoUserDto
    {
        public PermisoUserDto()
        {
            Roles = new List<PermisoRolDto>();
        }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<PermisoRolDto> Roles { get; set; }
    }

    public class PermisoRolDto
    {
        public PermisoRolDto()
        {
            MenusRole = new List<PermisoMenuRoleDto>();
            //Menus = new List<PermisoMenuLevel1Dto>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        //public List<PermisoMenuDto> Menus { get; set; }
        public List<PermisoMenuLevel1Dto> Menus { get; set; }
        [JsonIgnore]
        public List<PermisoMenuRoleDto> MenusRole { get; set; }

    }

    public class PermisoMenuRoleDto
    {
        public PermisoMenuRoleDto()
        {
            MenuAction = new PermisoMenuActionDto();
        }
        public int Id { get; set; }
        public int IdRole { get; set; }
        public int IdMenuAction { get; set; }
        [JsonIgnore]
        public byte State { get; set; }
        public PermisoMenuActionDto MenuAction { get; set; }
        //Artificio para filtrar solo menus que tiene permiso
        public int IdMenu { get; set; }
    }

    public class PermisoMenuActionDto {

        public PermisoMenuActionDto()
        {
            Menu = new PermisoMenuDto();
            Action = new PermisoActionDto();
        }
        public int Id { get; set; }
        public int IdMenu { get; set; }
        public int IdAction { get; set; }
        public byte State { get; set; }
        public PermisoMenuDto Menu { get; set; }
        public PermisoActionDto Action { get; set; }
        //Artificio para filtrar por rol
        public int IdRol { get; set; }
    }

    public class PermisoMenuDto
    {
        public PermisoMenuDto()
        {
            Actions = new List<PermisoActionDto>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public byte State { get; set; }

        public int? IdParent { get; set; }
        public int Level { get; set; }

        public List<PermisoActionDto> Actions { get; set; }
    }

    public class PermisoActionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    //=========================================================

    public class PermisoMenuLevelGenericDto
    {
        public PermisoMenuLevelGenericDto()
        {
            Actions = new List<PermisoActionDto>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public byte State { get; set; }
        [JsonIgnore]
        public int? IdParent { get; set; }
        public bool? IsForm { get; set; }
        public int Level { get; set; }
        public string Url { get; set; }
        public List<PermisoActionDto> Actions { get; set; }

    }

    public class PermisoMenuLevel1Dto : PermisoMenuLevelGenericDto
    {
        public PermisoMenuLevel1Dto()
        {
            Menus = new List<PermisoMenuLevel2Dto>();
        }
        public List<PermisoMenuLevel2Dto> Menus { get; set; }
    }

    public class PermisoMenuLevel2Dto : PermisoMenuLevelGenericDto
    {
        public PermisoMenuLevel2Dto()
        {
            Menus = new List<PermisoMenuLevel3Dto>();
        }

        public List<PermisoMenuLevel3Dto> Menus { get; set; }
    }

    public class PermisoMenuLevel3Dto : PermisoMenuLevelGenericDto
    {
        public PermisoMenuLevel3Dto()
        {
            Menus = new List<PermisoMenuLevel4Dto>();
        }

        public List<PermisoMenuLevel4Dto> Menus { get; set; }
    }

    public class PermisoMenuLevel4Dto : PermisoMenuLevelGenericDto
    {
        public PermisoMenuLevel4Dto()
        {
            Menus = new List<PermisoMenuLevel5Dto>();
        }

        public List<PermisoMenuLevel5Dto> Menus { get; set; }
    }

    public class PermisoMenuLevel5Dto : PermisoMenuLevelGenericDto
    {

    }

}
