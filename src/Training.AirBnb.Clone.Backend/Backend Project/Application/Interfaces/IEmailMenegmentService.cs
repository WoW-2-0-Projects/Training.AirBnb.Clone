using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Project.Application.Interfaces
{
    public interface IEmailMenegmentService
    {
        ValueTask<bool> SendEmailAsync(Guid userId, Guid templateId);
    }
}
