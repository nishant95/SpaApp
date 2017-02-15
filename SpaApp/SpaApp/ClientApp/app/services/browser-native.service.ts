import { OpaqueToken } from "@angular/core";

export let WINDOW_REF = new OpaqueToken("window_ref");

export interface IWindowRef {
    getNativeWindow();
}

export const WindowRef : IWindowRef = {

    getNativeWindow() {
        return window;
    }
}