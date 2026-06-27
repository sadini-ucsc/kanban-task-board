import Board from "../components/Board/Board";
import { useTasks } from "../context/TaskContext";

function KanbanPage() {

    const { loading, error } = useTasks();

    if (loading)
        return <p>Loading tasks...</p>;

    if (error)
        return <p>{error}</p>;

    return (

        <div className="container">

            <h1>Kanban Task Board</h1>

            <Board />

        </div>

    );

}

export default KanbanPage;