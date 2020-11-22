import { Component, OnDestroy, OnInit, ViewEncapsulation } from "@angular/core";
import {
    AbstractControl,
    FormBuilder,
    FormGroup,
    ValidationErrors,
    ValidatorFn,
    Validators,
} from "@angular/forms";
import { Subject } from "rxjs";

import { FuseConfigService } from "@fuse/services/config.service";
import { fuseAnimations } from "@fuse/animations";
import { Router } from "@angular/router";
import { UserService } from 'app/services/user.service';
import Register from 'app/model/register';
import swal from "sweetalert2";

@Component({
    selector: "register",
    templateUrl: "./register.component.html",
    styleUrls: ["./register.component.scss"],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations,
})
export class RegisterComponent implements OnInit, OnDestroy {
    registerForm: FormGroup;

    // Private
    private _unsubscribeAll: Subject<any>;

    constructor(
        private _fuseConfigService: FuseConfigService,
        private _formBuilder: FormBuilder,
        private userService: UserService,
        private router: Router
    ) {
        this._fuseConfigService.config = {
            layout: {
                navbar: {
                    hidden: true,
                },
                toolbar: {
                    hidden: true,
                },
                footer: {
                    hidden: true,
                },
                sidepanel: {
                    hidden: true,
                },
            },
        };

        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    ngOnInit(): void {
        this.registerForm = this._formBuilder.group({
            name: ["", Validators.required],
            email: ["", [Validators.required, Validators.email]],
            password: ["", Validators.required],
            passwordConfirm: ["", [Validators.required]],
        });
    }

    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    register(registerForm){
        if(registerForm.isValid === false){
            return;
        }

        let name = this.registerForm.get("name").value;
        let email = this.registerForm.get("email").value;
        let password = this.registerForm.get("password").value;
        let passwordConfirm = this.registerForm.get("passwordConfirm").value;

        if(password !== passwordConfirm){
            return;
        }

        let register = new Register();
        register.name = name;
        register.email = email;
        register.password = password;

        this.userService.register(register).subscribe(result => {
            this.router.navigate(["auth/login"]);
        }, (error) => {
            if(error.status === 404){
                swal.fire("Ops!","Email ou senha invalido", "error");
            }
        });
    }
}
