using Microsoft.AspNetCore.Mvc;

namespace NerdStore.WebApp.MVC.Controllers;

public abstract class ControllerBase : Controller
{
    protected Guid ClienteId = Guid.Parse("a21563ca-5e00-4302-ae5e-c5496fd5d535");
}