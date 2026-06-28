function ConfirmationModal({
    isOpen,
    title,
    message,
    onConfirm,
    onCancel
}) {

    if (!isOpen) {
        return null;
    }

    return (
        <div className="modal-overlay">

            <div className="modal">

                <h2>{title}</h2>

                <p>{message}</p>

                <div className="modal-actions">

                    <button
                        className="btn-danger"
                        onClick={onConfirm}
                    >
                        Delete
                    </button>

                    <button
                        className="btn-secondary"
                        onClick={onCancel}
                    >
                        Cancel
                    </button>

                </div>

            </div>

        </div>
    );
}

export default ConfirmationModal;