using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListApplication.Data;
using ToDoListApplication.Models;
using ToDoListApplication.Models.Entities;

namespace ToDoListApplication.Controllers;

public class ToDoListApplicationController : Controller
{
    
    private readonly ApplicationDbContext dbContext;
    
    public ToDoListApplicationController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    [HttpGet]
    // GET
    public IActionResult addnewtask()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> addnewtask(AddWorkTableViewModel viewModel)
    {
      
        Console.WriteLine(viewModel.Name);
        Console.WriteLine(viewModel.Description);
        Console.WriteLine("hello world");
        var task = new WorkTable {
            Name = viewModel.Name,
            Description= viewModel.Description,
            Status= viewModel.Status,
        };
        
        await dbContext.works.AddAsync(task);
        await dbContext.SaveChangesAsync();
        
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> tasks()
    {
        var tasks = await dbContext.works.ToListAsync();
        return View(tasks);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var tasks = await dbContext.works.FindAsync(id);
        return View(tasks);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(WorkTable viewModel)
    {
        var tasks = await dbContext.works.FindAsync(viewModel.Id);
        if (tasks is not null)
        {
            tasks.Name = viewModel.Name;
            tasks.Description = viewModel.Description;
            tasks.Status = viewModel.Status;
            
            await dbContext.SaveChangesAsync();
        }
        return RedirectToAction("tasks", "ToDoListApplication");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(WorkTable viewModel)
    {
        var tasks = await dbContext.works.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);
        if (tasks is not null)
        {
            dbContext.works.Remove(tasks);
            await dbContext.SaveChangesAsync();
        }
        return RedirectToAction("tasks", "ToDoListApplication");
    }

}