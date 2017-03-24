import { Injectable } from '@angular/core';
//import { OpaqueToken } from "@angular/core";

//export let WINDOW_REF = new OpaqueToken("window_ref");

export interface IWindow {
    getNativeWindow();
}

@Injectable()
export class WindowBrowser implements IWindow {

    getNativeWindow() {
        return window;
    }
}
