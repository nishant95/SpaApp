import { Component } from '@angular/core';
import { SecurityService } from '../../services/security.service';

@Component({
    selector: 'app',
    template: require('./app.component.html'),
    styles: [require('./app.component.css')]
})
export class AppComponent {
    constructor(private securityService: SecurityService) {
    }

    ngOnInit() {

        if (typeof window !== 'undefined' && window.location.hash) {
            console.log('appcomponent:ngOnInit _securityService.AuthorizedCallback');
            this.securityService.AuthorizedCallback();
        }
    }
}
