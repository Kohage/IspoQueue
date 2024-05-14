import React, { useEffect, useState } from 'react'
import { Container, Table } from 'reactstrap'
import styles from './display.module.css'
import fetchQueue from '../../services/getQueue.ts';
import axios from "axios";

interface Queue {
    id: string;
    number: string | null;
    creationTime: string;
    timeStart: string | null;
    timeEnd: string | null;
    statusId: number | null;
    serviceId: number;
    windowId: string | null;
}
function Display() {
    const [queue, setQueue] = useState<Queue[]>([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get('/api/queue'); // Замените '/queue' на путь к вашему контроллеру на бэкенде
                console.log(response.data);
                setQueue(response.data);
            } catch (error) {
                console.error('Ошибка при загрузке очереди:', error);
            }
        };

        fetchData();
    }, []);


    const q = queue.length > 0 ? queue.map(q => (
        <tr key={q.id}>
            <td>{q.number}</td>
            <td>{q.creationTime}</td>
            <td>{q.timeStart}</td>
            <td>{q.timeEnd}</td>
            <td>{q.statusId}</td>
            <td>{q.serviceId}</td>
            <td>{q.windowId}</td>
        </tr>
    )) : <></>

    return (
        <Container>
            <Container>
                <table className={styles.table}>
                    <thead>
                    <tr>
                        <th>№ талона</th>
                        <th>Дата создания</th>
                        <th>Начало</th>
                        <th>Конец</th>
                        <th>Статус</th>
                        <th>Услуга</th>
                        <th>Окно</th>
                    </tr>
                    </thead>
                    <tbody>
                    {q}
                    </tbody>
                </table>
            </Container>
        </Container>
    )
}

export default Display