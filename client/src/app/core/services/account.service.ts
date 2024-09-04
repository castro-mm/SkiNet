import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { User } from '../../shared/models/user';
import { Address } from '../../shared/models/address';
import { map, tap } from 'rxjs';
import { SignalrService } from './signalr.service';

@Injectable({
    providedIn: 'root'
})
export class AccountService {
    baseApiUrl: string = environment.apiUrl;
    currentUser = signal<User|null>(null);

    constructor(private http: HttpClient, private signalrService: SignalrService) { }

    login(values: any) {
        let params = new HttpParams();
        params = params.append('useCookies', true);

        return this.http.post<User>(`${this.baseApiUrl}login`, values, { params })
            .pipe(
                tap(() => this.signalrService.createHubConnection())
            )
        ;
    }

    register(values: any) {
        return this.http.post(`${this.baseApiUrl}account/register`, values);
    }

    logout() {
        return this.http.post(`${this.baseApiUrl}account/logout`, {})
            .pipe(
                tap(() => this.signalrService.stopHubConnection())
            );
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
        return this.http.post<Address>(`${this.baseApiUrl}account/address`, address).pipe(
            tap(() => {
                this.currentUser.update((user: User | null) => {
                    if (user) user.address = address;
                    return user;
                })
            })
        );
    }

    getAuthState() {
        return this.http.get<{isAuthenticated: boolean}>(`${this.baseApiUrl}account/auth-status`)
    }
}
