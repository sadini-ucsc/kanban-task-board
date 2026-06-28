import { useState } from "react";
import { DndContext, closestCorners } from "@dnd-kit/core";

import { useTasks } from "../context/TaskContext";
import Board from "../components/Board/Board";
import TaskModal from "../components/TaskModal/TaskModal";
import "./KanbanPage.css";
import { UI_TEXT } from "../constants/uiText";

function KanbanPage() {

    const {
        tasks,
        loading,
        error
    } = useTasks();

    const [isModalOpen, setIsModalOpen] = useState(false);
    const [selectedTask, setSelectedTask] = useState(null);

    const { updateTask } = useTasks();

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

    async function handleDragEnd(event) {

        const { active, over } = event;

        if (!over) return;

        const taskId = active.id;
        const newStatus = over.id;

        const task = tasks.find(t => t.id === taskId);

        if (!task || task.status === newStatus) return;

        await updateTask({
            ...task,
            status: Number(newStatus)
        });
    }

    return (
        <div className="kanban-page">

            <h1>{UI_TEXT.KANBAN_BOARD_PAGE_TITLE}</h1>

            {error && (
                <div className="error">
                    {error}
                </div>
            )}

            <button
                type="button"
                className="btn-primary"
                onClick={handleAddTask}
            >
                {UI_TEXT.BUTTONS.ADD_TASK}
            </button>            

            <br />
            <br />

            {loading ? (
                <p>{UI_TEXT.MESSAGES.LOADING_TASKS}</p>
            ) : (
                <DndContext
                    collisionDetection={closestCorners}
                    onDragEnd={handleDragEnd}
                >
                    <Board
                        tasks={tasks}
                        onEdit={handleEdit}
                    />
                </DndContext>
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