import { Injectable } from '@angular/core';
import { Payload } from '../interfaces/payload';

@Injectable({
    providedIn: 'root'
})
export class SharedLocalStorageService {
    constructor() { }

    KEY_TOKEN = 'token';

    SetToken(token: string) {
        localStorage.setItem(this.KEY_TOKEN, token);
    }
    GetToken(): string {
        return localStorage.getItem(this.KEY_TOKEN);
    }
    GetTokenPayload(): Payload {
        let tokenData = this.GetToken();
        let payloadData = JSON.parse(window.atob(tokenData.split('.')[1])) as Payload;

        return payloadData;
    }
}
