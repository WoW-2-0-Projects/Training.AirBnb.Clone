using AirBnB.Application.Common.Identity.Models;
using AirBnB.Domain.Common.Commands;
using AirBnB.Domain.Entities;

namespace AirBnB.Application.Common.Identity.Commands;

/// <summary>
/// Represents sign in command
/// </summary>
public class SignInCommand : ICommand<(AccessToken accessToken, RefreshToken refreshToken)>
{
    /// <summary>
    /// Sign in by email method
    /// </summary>
    public SignInByEmailDetails? SignInByEmail { get; set; }   
    
    /// <summary>
    /// Sign in by phone method
    /// </summary>
    public SignInByPhoneDetails? SignInByPhone { get; set; } 
}