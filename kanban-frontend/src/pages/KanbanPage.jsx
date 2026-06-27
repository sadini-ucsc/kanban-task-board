import { useState } from "react";
import { useTasks } from "../context/TaskContext";

import Board from "../components/Board/Board";
import TaskModal from "../components/TaskModal/TaskModal";

function KanbanPage() {

    const {
        tasks,
        loading,
        error
    } = useTasks();

    const [isModalOpen, setIsModalOpen] = useState(false);
    const [selectedTask, setSelectedTask] = useState(null);

    function handleAddTask() {
        setSelectedTask(null);
        setIsModalOpen(true);
    }

    function handleEdit(task) {
        setSelectedTask(task);
        setIsModalOpen(true);
    }

    function handleCloseModal() {
        setIsModalOpen(false);
        setSelectedTask(null);
    }

    return (
        <div className="kanban-page">

            <h1>Kanban Task Board</h1>

            {error && (
                <div className="error">
                    {error}
                </div>
            )}

            <button
                type="button"
                onClick={handleAddTask}
            >
                + Add Task
            </button>

            <br />
            <br />

            {loading ? (
                <p>Loading tasks...</p>
            ) : (
                <Board
                    tasks={tasks}
                    onEdit={handleEdit}
                />
            )}

            <TaskModal
                isOpen={isModalOpen}
                task={selectedTask}
                onClose={handleCloseModal}
            />

        </div>
    );
}

export default KanbanPage;