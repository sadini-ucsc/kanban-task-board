import { useState } from "react";
import { useDraggable } from "@dnd-kit/core";
import { LuGripVertical } from "react-icons/lu";

import ConfirmationModal from "../ConfirmationModal/ConfirmationModal";
import { useTasks } from "../../context/TaskContext";
import "./TaskCard.css";
import { UI_TEXT } from "../../constants/uiText";

function TaskCard({ task, onEdit }) {

    const { deleteTask } = useTasks();

    const [showDeleteModal, setShowDeleteModal] = useState(false);

    const { attributes, listeners, setNodeRef, transform, isDragging } =
        useDraggable({
            id: task.id
        });

    const style = {
        transform: transform
            ? `translate(${transform.x}px, ${transform.y}px)`
            : undefined,
        opacity: isDragging ? 0.5 : 1
    };

    async function handleDelete() {

        try {

            const deleted = await deleteTask(task.id);

            if (deleted) {
                setShowDeleteModal(false);
            }

        } catch (error) {

            console.error(error);

            alert(UI_TEXT.MESSAGES.DELETE_ERROR);
        }
    }

    return (
        <div
            ref={setNodeRef}
            style={style}
            className="task-card"
        >
            <div
                className="drag-handle"
                {...listeners}
                {...attributes}
                title={UI_TEXT.TOOLTIPS.DRAG_TASK}
            >
                <LuGripVertical size={20} />
            </div>

            <h3>{task.title}</h3>

            <p>{task.description}</p>

            <div className="task-card-dates">

                <div>
                    <strong>{UI_TEXT.TASK_CARD.CREATED}</strong> {new Date(task.createdAt).toLocaleString()}
                </div>

                <div>
                    <strong>{UI_TEXT.TASK_CARD.UPDATED}</strong> {new Date(task.updatedAt).toLocaleString()}
                </div>

            </div>

            <div className="task-card-actions">

                <button
                    type="button"
                    className="btn-secondary"
                    onClick={() => onEdit(task)}
                >
                    {UI_TEXT.BUTTONS.EDIT}
                </button>

                <button
                    type="button"
                    className="btn-danger"
                    onClick={() => setShowDeleteModal(true)}
                >
                    {UI_TEXT.BUTTONS.DELETE}
                </button>

            </div>

            <ConfirmationModal
                isOpen={showDeleteModal}
                title={UI_TEXT.MODALS.DELETE_TITLE}
                message={UI_TEXT.MODALS.DELETE_MESSAGE}
                onConfirm={handleDelete}
                onCancel={() => setShowDeleteModal(false)}
            />

        </div>
    );
}

export default TaskCard;