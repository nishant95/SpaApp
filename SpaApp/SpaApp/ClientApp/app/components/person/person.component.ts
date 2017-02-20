import { Component } from '@angular/core';
import { SecurityService } from '../../services';

@Component({
    selector: 'person',
    template: require('./person.component.html'),
    styles: [require('./person.component.css')]
})
export class PersonComponent {
    constructor(private securityService: SecurityService) {

    }
}
