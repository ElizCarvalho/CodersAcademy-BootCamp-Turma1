import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import Swal from "sweetalert2";
import { PersistedStateService } from 'app/services/persisted-state.service';
import User from 'app/model/user';
import { UserService } from 'app/services/user.service';

@Component({
    selector: "app-favorite",
    templateUrl: "./favorite.component.html",
    styleUrls: ["./favorite.component.scss"],
})
export class FavoriteComponent implements OnInit {

    user: User;

    displayedColumns: string[] = [
        "position",
        "name",
        "band",
        "avatar",
        "album",
        "action",
    ];
    dataSource: any[];

    constructor(
        private persistedState: PersistedStateService,
        private router: Router,
        private userService: UserService
    ) {}

    ngOnInit() {
        this.user = this.persistedState.get(this.persistedState.LOGGEND_IN);
        this.dataSource = (this.user.favoriteMusics as any);
    }

    goToDetail(albumId){
        this.router.navigate(["music", albumId]);
    }

    removeFromFavorite(musicId){
        this.userService
        .removeFromFavorite(this.user.id, musicId)
        .subscribe(() => {
            this.user.favoriteMusics = this.user.favoriteMusics.filter(
                x => x.musicId !== musicId
            );
            this.persistedState.set(
                this.persistedState.LOGGEND_IN, 
                this.user
            );
            this.dataSource = this.user.favoriteMusics as any;
            Swal.fire(
                "Sucesso",
                "Musica removida dos favoritos",
                "success"
            )
        })
    }
}
