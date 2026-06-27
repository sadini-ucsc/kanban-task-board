import { useTasks } from "../../context/TaskContext";
import TaskCard from "../TaskCard/TaskCard";

function Column({ label, status }) {

    const { tasks } = useTasks();

    const filteredTasks = tasks.filter(
        task => task.status === status
    );

    return (
        <div className="board-column">

            <h2>{label}</h2>

            {filteredTasks.length === 0 && (
                <p style={{ color: "#888" }}>No tasks</p>
            )}

            {filteredTasks.map(task => (
                <TaskCard
                    key={task.id}
                    task={task}
                />
            ))}

        </div>
    );
}

export default Column;