import TaskCard from "../TaskCard/TaskCard";
import { useDroppable } from "@dnd-kit/core";
import { KanbanTaskStatus } from "../../enums/KanbanTaskStatus";

function Column({ title, status, tasks, onEdit }) {

    const { setNodeRef } = useDroppable({
        id: status
    });

    return (
        <div className="board-column" ref={setNodeRef}>

            <h2>{title}</h2>

            {tasks.map(task => (
                <TaskCard
                    key={task.id}
                    task={task}
                    onEdit={onEdit}
                />
            ))}

        </div>
    );
}

export default Column;