﻿namespace ShellAndNecklaceAPI.Data.DTOs
{
    public class AccountDTO
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public bool? Verified { get; set; }
    }
}
