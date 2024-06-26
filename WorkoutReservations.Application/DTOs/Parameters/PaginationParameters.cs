﻿namespace WorkoutReservations.Application.DTOs.Parameters
{
    public class PaginationParameters
    {
        private const int maxPageSize = 12;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 6;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
