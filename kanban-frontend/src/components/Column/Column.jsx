import TaskCard from "../TaskCard/TaskCard";

function Column({ title, tasks, onEdit }) {

    return (
        <div className="board-column">

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