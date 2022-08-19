using System.Collections.Generic;

namespace Security.Application.Dto.Base.PrimeNG
{
    public class PrimeTableDto
    {
        /// <summary>
        /// Primera fila
        /// </summary>
        public int First { get; set; }

        /// <summary>
        /// Número de filas por página
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// Nombre de campo para ordenar
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        /// Tipo de ordenamiento: 1 => Asc, -1 => Desc
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Filtra el objeto que tiene el campo como clave y el valor del filtro
        /// </summary>
        public Dictionary<string, PrimeFilter> Filters { get; set; }

        /// <summary>
        /// Valor del filtro global si está disponible
        /// </summary>
        public string GlobalFilter { get; set; }
    }
}
