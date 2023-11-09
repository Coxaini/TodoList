import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../../environments/environment";

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {

    baseUrl = environment.apiUrl + '/auth';

    constructor(private httpClient: HttpClient) {
    }

    public register(username: string, password: string) {
        return this.httpClient.post(this.baseUrl + '/register', {username, password}, {withCredentials: true});
    }

    public login(username: string, password: string) {
        return this.httpClient.post(this.baseUrl + '/login', {username, password}, {withCredentials: true});
    }

    public refreshToken() {
        return this.httpClient.post(this.baseUrl + '/refresh', {}, {withCredentials: true});
    }

    public logout() {
        return this.httpClient.post(this.baseUrl + '/logout', {}, {withCredentials: true});
    }

}
