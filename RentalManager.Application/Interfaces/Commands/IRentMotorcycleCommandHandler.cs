using RentalManager.Application.Commands.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalManager.Application.Interfaces.Commands;

public interface IRentMotorcycleCommandHandler
{
    Task Handle(RentMotorcycleCommandRequest request);
}
