import { useTasks } from "../../context/TaskContext";

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
                <div key={task.id} className="task-card">
                    <strong>{task.title}</strong>
                    <p>{task.description}</p>
                </div>
            ))}

        </div>
    );
}

export default Column;