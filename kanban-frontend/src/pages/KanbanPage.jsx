import { useTasks } from "../context/TaskContext";

function KanbanPage() {

    const { tasks, loading, error } = useTasks();

    if (loading)
        return <p>Loading tasks...</p>;

    if (error)
        return <p>{error}</p>;

    return (

        <div className="container">

            <h1>Kanban Task Board</h1>

            <pre>

                {JSON.stringify(tasks, null, 2)}

            </pre>

        </div>

    );

}

export default KanbanPage;