using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.shared.Entities;

namespace Orders.Backend.Controllers;

public class CategoriesController : GenericController<Category>
{
    public CategoriesController(IGenericUnitOfWork<Category> unitOfWork) : base(unitOfWork)
    {
    }
}