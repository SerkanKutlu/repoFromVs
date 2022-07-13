﻿using System.ComponentModel.DataAnnotations;

namespace IdentityManagementServer.Model;

public class RegisterRequestModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}