﻿namespace WienerOsiguranjeTehnickiZadatak.Models
{
    public class PaginatedPartnersViewModel
    {
        public IEnumerable<Partner> Partners { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
