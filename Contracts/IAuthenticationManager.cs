using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObjects;

namespace Contracts;

public interface IAuthenticationManager
{
	Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
	Task<string> CreateToken();
}