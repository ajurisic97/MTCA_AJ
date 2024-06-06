using MTCA.Application.Commons.Abstractions;
using MTCA.Application.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Identity.Users.Login;
public sealed record LoginUserCommand(
    string Username,
    string Password) : ICommand<CommandResponse<TokenResponse>>;