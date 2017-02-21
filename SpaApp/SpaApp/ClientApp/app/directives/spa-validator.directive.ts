import { Directive, forwardRef } from '@angular/core';
import { NG_VALIDATORS, FormControl } from '@angular/forms';


function validateEmailFactory() {
    return (c: FormControl) => {
        let EMAIL_REGEXP = /^[a - z0 - 9!#$ %&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&' * +/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/;
        
        return EMAIL_REGEXP.test(c.value) ? null : {
            validateEmail: {
                valid: false
            }
        };
    };
}

@Directive({
    selector: '[validateEmail][ngModel],[validateEmail][formControl]',
    providers: [
        { provide: NG_VALIDATORS, useExisting: forwardRef(() => SpaEmailValidator), multi: true }
    ]
})
export class SpaEmailValidator {

    validator: Function;

    constructor() {
        this.validator = validateEmailFactory();
    }

    validate(c: FormControl) {
        return this.validator(c);
    }
}