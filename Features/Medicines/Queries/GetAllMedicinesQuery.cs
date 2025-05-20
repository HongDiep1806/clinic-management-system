using ClinicManagementSystem.DTOs.Medicine;
using MediatR;

namespace ClinicManagementSystem.Features.Medicines.Queries
{
    public class GetAllMedicinesQuery : IRequest<List<MedicineDto>> { }

}
