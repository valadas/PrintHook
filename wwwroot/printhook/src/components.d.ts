/* eslint-disable */
/* tslint:disable */
/**
 * This is an autogenerated file created by the Stencil compiler.
 * It contains typing information for all components that exist in this project.
 */
import { HTMLStencilElement, JSXBase } from "@stencil/core/internal";
export namespace Components {
    interface PhApp {
    }
}
declare global {
    interface HTMLPhAppElement extends Components.PhApp, HTMLStencilElement {
    }
    var HTMLPhAppElement: {
        prototype: HTMLPhAppElement;
        new (): HTMLPhAppElement;
    };
    interface HTMLElementTagNameMap {
        "ph-app": HTMLPhAppElement;
    }
}
declare namespace LocalJSX {
    interface PhApp {
    }
    interface IntrinsicElements {
        "ph-app": PhApp;
    }
}
export { LocalJSX as JSX };
declare module "@stencil/core" {
    export namespace JSX {
        interface IntrinsicElements {
            "ph-app": LocalJSX.PhApp & JSXBase.HTMLAttributes<HTMLPhAppElement>;
        }
    }
}
