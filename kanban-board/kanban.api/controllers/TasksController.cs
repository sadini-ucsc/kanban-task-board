using kanban.application.Dtos;
using kanban.application.Services;
using Microsoft.AspNetCore.Mvc;

namespace kanban.api.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IKanbanTaskService taskService;

        public TasksController(IKanbanTaskService taskService)
        {
            this.taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<KanbanTaskDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var dto = await taskService.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

            if (dto is null)
                return NotFound();

            return Ok(dto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KanbanTaskDto>>> GetAll(CancellationToken cancellationToken)
        {
            var dtos = await taskService.GetAllAsync(cancellationToken).ConfigureAwait(false);

            return Ok(dtos);
        }
    }
}
