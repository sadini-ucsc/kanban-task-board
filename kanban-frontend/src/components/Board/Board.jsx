import Column from "../Column/Column";
import { KanbanTaskStatus } from "../../enums/KanbanTaskStatus";
import "./Board.css";

function Board({ tasks, onEdit }) {

    const todoTasks = tasks.filter(
        task => task.status === KanbanTaskStatus.Todo.value
    );

    const inProgressTasks = tasks.filter(
        task => task.status === KanbanTaskStatus.InProgress.value
    );

    const doneTasks = tasks.filter(
        task => task.status === KanbanTaskStatus.Done.value
    );

    return (
        <div className="board">

            <Column
                title={KanbanTaskStatus.Todo.label}
                status={KanbanTaskStatus.Todo.value}
                tasks={todoTasks}
                onEdit={onEdit}
            />

            <Column
                title={KanbanTaskStatus.InProgress.label}
                status={KanbanTaskStatus.InProgress.value}
                tasks={inProgressTasks}
                onEdit={onEdit}
            />

            <Column
                title={KanbanTaskStatus.Done.label}
                status={KanbanTaskStatus.Done.value}
                tasks={doneTasks}
                onEdit={onEdit}
            />

        </div>
    );
}

export default Board;