import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { User } from '../../shared/models/user';
import { Address } from '../../shared/models/address';
import { map } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AccountService {
    baseApiUrl: string = environment.apiUrl;
    currentUser = signal<User|null>(null);

    constructor(private http: HttpClient) { }

    login(values: any) {
        let params = new HttpParams();
        params = params.append('useCookies', true);

        return this.http.post<User>(`${this.baseApiUrl}login`, values, { params });
    }

    register(values: any) {
        return this.http.post(`${this.baseApiUrl}account/register`, values);
    }

    logout() {
        return this.http.post(`${this.baseApiUrl}account/logout`, {});
    }

    getUserInfo() {
        return this.http.get<User>(`${this.baseApiUrl}account/user-info`).pipe(
            map(user => {
                this.currentUser.set(user);
                return user;
            })
        )
    }

    updateAddress(address: Address) {
        return this.http.post<Address>(`${this.baseApiUrl}account/address`, address);
    }

    getAuthState() {
        return this.http.get<{isAuthenticated: boolean}>(`${this.baseApiUrl}account/auth-status`)
    }
}
