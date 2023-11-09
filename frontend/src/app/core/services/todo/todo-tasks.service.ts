import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../../environments/environment";
import {Task} from "../../models/task";

@Injectable({
    providedIn: 'root'
})
export class TodoTasksService {


    baseUrl = environment.apiUrl + '/tasks';

    constructor(private httpClient: HttpClient) {
    }

    public getTasks() {
        return this.httpClient.get<Task[]>(this.baseUrl, {withCredentials: true});
    }

    public createTask(title: string) {
        return this.httpClient.post<Task>(this.baseUrl, {title}, {withCredentials: true});
    }

    public updateTask(id: string, title: string, completed: boolean) {
        return this.httpClient.put<Task>(this.baseUrl + '/' + id, {title, isCompleted: completed}, {withCredentials: true});
    }

    public deleteTask(id: string) {
        return this.httpClient.delete(this.baseUrl + '/' + id, {withCredentials: true});
    }
}
