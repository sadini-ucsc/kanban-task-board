import { useState } from "react";
import { useTasks } from "../context/TaskContext";

import Board from "../components/Board/Board";
import TaskModal from "../components/TaskModal/TaskModal";

function KanbanPage() {

    const { loading, error } = useTasks();

    const [isModalOpen, setIsModalOpen] = useState(false);

    return (
        <div className="kanban-page">

            <h1>Kanban Task Board</h1>

            {error && <div className="error">{error}</div>}

            <button onClick={() => setIsModalOpen(true)}>
                + Add Task
            </button>

            <br/>
            <br/>

            {loading ? (
                <p>Loading tasks...</p>
            ) : (
                <Board />
            )}

            <TaskModal
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
            />

        </div>
    );
}

export default KanbanPage;