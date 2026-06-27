import { useEffect, useState } from "react";
import { getTasks } from "../api/taskApi";

function KanbanPage() {

    const [tasks, setTasks] = useState([]);

    useEffect(() => {

        async function loadTasks() {

            try {

                const data = await getTasks();
                setTasks(data);

            } catch (error) {

                console.error(error);

            }

        }

        loadTasks();

    }, []);

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