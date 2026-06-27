function TaskCard({ task }) {

    return (
        <div className="task-card">

            <h3>{task.title}</h3>

            <p>{task.description}</p>

            <div className="task-card-dates">

                <div>
                    <strong>Created:</strong> {new Date(task.createdAt).toLocaleString()}
                </div>

                <div>
                    <strong>Updated:</strong> {new Date(task.updatedAt).toLocaleString()}
                </div>

            </div>

        </div>
    );
}

export default TaskCard;