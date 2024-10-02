using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MTCA.Application.Identity.Users.RefreshToken;
public sealed record RefreshTokenCommand(
    string Token, 
    string RefreshToken) : ICommand<CommandResponse<TokenResponse>>;