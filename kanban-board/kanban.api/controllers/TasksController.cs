using kanban.application.dtos;
using kanban.application.services;
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
            var dto = await taskService.GetByIdAsync(id, cancellationToken);

            return Ok(dto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KanbanTaskDto>>> GetAll(CancellationToken cancellationToken)
        {
            var dtos = await taskService.GetAllAsync(cancellationToken);

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<KanbanTaskDto>> Create(CreateKanbanTaskRequest request, CancellationToken cancellationToken)
        {
            var dto = await taskService.CreateAsync(request, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<KanbanTaskDto>> Update(Guid id, UpdateKanbanTaskRequest request, CancellationToken cancellationToken)
        {
            var dto = await taskService.UpdateAsync(id, request, cancellationToken);

            return Ok(dto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var deleted = await taskService.SoftDeleteAsync(id, cancellationToken);

            return Ok(deleted);
        }
    }
}
