import { Component, OnInit } from '@angular/core';
import { MusicService } from 'app/services/music.service';
import { ActivatedRoute, Router } from '@angular/router';
import Album from 'app/model/album';
import { PersistedStateService } from 'app/services/persisted-state.service';
import { UserService } from 'app/services/user.service';
import User from 'app/model/user';

@Component({
  selector: 'app-music-detail',
  templateUrl: './music-detail.component.html',
  styleUrls: ['./music-detail.component.scss']
})
export class MusicDetailComponent implements OnInit {

  albumId: String = undefined;
  album: Album = undefined;
  user: User;

  constructor(
    private musicService: MusicService, 
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private persistedState: PersistedStateService,
    private userService: UserService
  ) { }

  ngOnInit() {
    this.albumId = this.activatedRoute.snapshot.paramMap.get("id");
    this.musicService.getAlbumDetails(this.albumId).subscribe(data => {
      this.album = data;
    });

    this.user = this.persistedState.get(this.persistedState.LOGGEND_IN);
  }

  back(){
    this.router.navigate(["music"]);
  }

  isFavoriteMusic(musicId){
    var pepe = this.user.favoriteMusics.findIndex((x) => x.musicId == musicId) >= 0;
    return this.user.favoriteMusics.findIndex((x) => x.musicId == musicId) >= 0;
  }

  toogleFavorite(musicId){
    if(this.isFavoriteMusic(musicId) === false) {
      this.addToFavorite(musicId)
    }
    else{
      this.removeFromFavorite(musicId);
    }
  }

  private addToFavorite(musicId: any): any {
    this.userService
    .addToFavorite(this.user.id, musicId)
    .subscribe((data) => {
      this.userService.getUser(this.user.id).subscribe((data) => {
        this.user = data,
        this.persistedState.set(
          this.persistedState.LOGGEND_IN,
          this.user
        );
      });
    });
  }

  private removeFromFavorite(musicId: any) {
    this.userService
    .removeFromFavorite(this.user.id, musicId)
    .subscribe((data) => {
      this.user.favoriteMusics = this.user.favoriteMusics.filter(
          (x) => x.musicId !== musicId
        );
      this.persistedState.set(this.persistedState.LOGGEND_IN, this.user);
    })
  }
}
