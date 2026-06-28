import { useState } from "react";
import { useDraggable } from "@dnd-kit/core";
import { LuGripVertical } from "react-icons/lu";

import ConfirmationModal from "../ConfirmationModal/ConfirmationModal";
import { useTasks } from "../../context/TaskContext";

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

            alert("Unable to delete task.");
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
                title="Drag to move task"
            >
                <LuGripVertical size={20} />
            </div>

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

            <br />

            <div className="task-card-actions">

                <button
                    type="button"
                    className="btn-secondary"
                    onClick={() => onEdit(task)}
                >
                    Edit
                </button>

                <button
                    type="button"
                    className="btn-danger"
                    onClick={() => setShowDeleteModal(true)}
                >
                    Delete
                </button>

            </div>

            <ConfirmationModal
                isOpen={showDeleteModal}
                title="Delete Task"
                message="Are you sure you want to delete this task?"
                onConfirm={handleDelete}
                onCancel={() => setShowDeleteModal(false)}
            />

        </div>
    );
}

export default TaskCard;