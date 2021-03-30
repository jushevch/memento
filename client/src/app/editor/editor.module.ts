import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

import { NgbTooltipModule } from "@ng-bootstrap/ng-bootstrap";
import { EditorServicesModule } from "./editor-services.module";

import { EditorComponent } from "./editor.component";
import { RulerComponent } from "./components/ruler/ruler.component";
import { AreaInfoComponent } from "./components/area-info/area-info.component";
import { RoomListComponent } from "./components/room-list/room-list.component";
import { ItemListComponent } from "./components/item-list/item-list.component";
import { MobileListComponent } from "./components/mobile-list/mobile-list.component";
import { RoomComponent } from "./components/room/room.component";
import { RoomExitsComponent } from "./components/room-exits/room-exits.component";
import { RoomResetsComponent } from "./components/room-resets/room-resets.component";
import { ItemComponent } from "./components/item/item.component";
import { MobileComponent } from "./components/mobile/mobile.component";

import { NoTitlePipe } from "./pipes/no-title.pipe";
import { DirNamePipe } from "./pipes/dir-name.pipe";
import { DirValuePipe } from "./pipes/dir-value.pipe";
import { GenderNamePipe } from "./pipes/gender-name.pipe";
import { GenderValuePipe } from "./pipes/gender-value.pipe";

@NgModule({
    declarations: [
        EditorComponent,
        RulerComponent,
        AreaInfoComponent,
        RoomListComponent,
        ItemListComponent,
        MobileListComponent,
        RoomComponent,
        RoomExitsComponent,
        RoomResetsComponent,
        ItemComponent,
        MobileComponent,
        NoTitlePipe,
        DirNamePipe,
        DirValuePipe,
        GenderNamePipe,
        GenderValuePipe,
    ],
    imports: [
        CommonModule,
        FormsModule,
        NgbTooltipModule,
        EditorServicesModule,
    ],
    exports: [
        EditorComponent
    ]
})
export class EditorModule { }
