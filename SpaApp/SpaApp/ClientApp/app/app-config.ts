import { OpaqueToken } from "@angular/core";

export let APP_CONFIG = new OpaqueToken("app-config");

export interface IAppConfig {
    appBaseUrl: string;

    apiEndpoint: string;

    authServer: string;
    authorizationUrl: string;
    tokenUrl: string;
    userInfoUrl: string;
}

export const AppConfig: IAppConfig = {
    appBaseUrl: 'http://localhost:65035/',

    apiEndpoint: 'https://localhost:44315/api/',

    authServer: 'http://localhost:44313/',
    authorizationUrl: 'https://localhost:44313/connect/authorize',
    tokenUrl: 'https://localhost:44313/connect/token',
    userInfoUrl: 'https://localhost:44313/connect/userinfo'
};
