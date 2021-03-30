import { Component, OnInit } from "@angular/core";

import { VnumService } from "./services/vnum.service";
import { FileService } from "./services/file-service";

import { Area } from "./models/area.model";
import { Room } from "./models/room.model";
import { Item } from "./models/item.model";
import { Mobile } from "./models/mobile.model";
import { Name } from "./models/name.model";

@Component({
    selector: "app-editor",
    templateUrl: "./editor.component.html",
    styleUrls: ["./editor.component.css"]
})
export class EditorComponent implements OnInit
{
    area: Area = new Area();

    currentRoom: Room = null;
    firstFreeRoomVnum: number;
    userSetRoomVnum: number;

    currentItem: Item = null;
    firstFreeItemVnum: number;
    userSetItemVnum: number;

    currentMobile: Mobile = null;
    firstFreeMobileVnum: number;
    userSetMobileVnum: number;

    currentPanel: Panel = Panel.Info;

    constructor(
        private readonly vnumService: VnumService,
        private readonly fileService: FileService,
    ) { }

    ngOnInit(): void
    {
        this.setNextVnums();
    }

    /* Panels */

    goInfoPanel(): void
    {
        this.currentPanel = Panel.Info;
    }

    goRoomsPanel(): void
    {
        this.currentPanel = Panel.Rooms;
    }

    goItemsPanel(): void
    {
        this.currentPanel = Panel.Items;
    }

    goMobilesPanel(): void
    {
        this.currentPanel = Panel.Mobiles;
    }

    /* Info */

    createArea(): void
    {
        this.area = new Area();
        this.clearCurrentEntities();
    }

    uploadArea(blob: Blob): void
    {
        this.fileService.loadArea(blob, this.area);
        this.clearCurrentEntities();
    }

    downloadArea(): void
    {
        this.fileService.saveArea(this.area);
    }

    /* Rooms */

    editRoom(vnum: number): void
    {
        this.setCurrentRoom(vnum);
        this.goRoomsPanel();
    }

    setCurrentRoom(vnum: number): void
    {
        this.currentRoom = this.area.rooms.get(vnum);
    }

    clearCurrentRoom(): void
    {
        this.currentRoom = null;
    }

    isNewRoomVnumDisabled(): boolean
    {
        return this.firstFreeRoomVnum < 1;
    }

    isCreateRoomDisabled(): boolean
    {
        if (this.userSetRoomVnum == undefined)
        {
            return this.firstFreeRoomVnum < 1;
        }

        if (this.userSetRoomVnum >= this.area.info.minVnum &&
            this.userSetRoomVnum <= this.area.info.maxVnum &&
            this.vnumService.isVnumFree(this.userSetRoomVnum, this.area.rooms.keys()))
        {
            return false;
        }

        return true;
    }

    createRoom(): number
    {
        let room = new Room();
        room.vnum = this.userSetRoomVnum != undefined ? this.userSetRoomVnum : this.firstFreeRoomVnum;
        this.area.rooms.set(room.vnum, room);

        this.setNextRoomVnum();

        return room.vnum;
    }

    cloneRoom(vnum: number): void
    {
        let origin = this.area.rooms.get(vnum);
        let clone = this.area.rooms.get(this.createRoom());

        clone.title = origin.title;
        clone.look = origin.look;
    }

    setNextRoomVnum(): void
    {
        this.firstFreeRoomVnum = this.getFirstFreeVnum(this.area.rooms.keys());
        this.userSetRoomVnum = this.firstFreeRoomVnum < 0 ? undefined : this.firstFreeRoomVnum;
    }

    isPrevRoomDisabled(): boolean
    {
        return !this.area.rooms.has(this.currentRoom.vnum - 1);
    }

    isNextRoomDisabled(): boolean
    {
        return !this.area.rooms.has(this.currentRoom.vnum + 1);
    }

    goPrevRoom(): void
    {
        this.currentRoom = this.area.rooms.get(this.currentRoom.vnum - 1);
    }

    goNextRoom(): void
    {
        this.currentRoom = this.area.rooms.get(this.currentRoom.vnum + 1);
    }

    /* Items */

    editItem(vnum: number): void
    {
        this.setCurrentItem(vnum);
        this.goItemsPanel();
    }

    setCurrentItem(vnum: number): void
    {
        this.currentItem = this.area.items.get(vnum);
    }

    clearCurrentItem(): void
    {
        this.currentItem = null;
    }

    isNewItemVnumDisabled(): boolean
    {
        return this.firstFreeItemVnum < 1;
    }

    isCreateItemDisabled(): boolean
    {
        if (this.userSetItemVnum == undefined)
        {
            return this.firstFreeItemVnum < 1;
        }

        if (this.userSetItemVnum >= this.area.info.minVnum &&
            this.userSetItemVnum <= this.area.info.maxVnum &&
            this.vnumService.isVnumFree(this.userSetItemVnum, this.area.items.keys()))
        {
            return false;
        }

        return true;
    }

    createItem(): number
    {
        let item = new Item();
        item.vnum = this.userSetItemVnum != undefined ? this.userSetItemVnum : this.firstFreeItemVnum;
        this.area.items.set(item.vnum, item);

        this.setNextItemVnum();

        return item.vnum;
    }

    cloneItem(vnum: number): void
    {
        let origin = this.area.items.get(vnum);
        let clone = this.area.items.get(this.createItem());

        clone.keywords = origin.keywords;
        clone.gender = origin.gender;
        clone.name = new Name(origin.name);
        clone.look = origin.look;
        clone.description = origin.description;
    }

    setNextItemVnum(): void
    {
        this.firstFreeItemVnum = this.getFirstFreeVnum(this.area.items.keys());
        this.userSetItemVnum = this.firstFreeItemVnum < 0 ? undefined : this.firstFreeItemVnum;
    }

    isPrevItemDisabled(): boolean
    {
        return !this.area.items.has(this.currentItem.vnum - 1);
    }

    isNextItemDisabled(): boolean
    {
        return !this.area.items.has(this.currentItem.vnum + 1);
    }

    goPrevItem(): void
    {
        this.currentItem = this.area.items.get(this.currentItem.vnum - 1);
    }

    goNextItem(): void
    {
        this.currentItem = this.area.items.get(this.currentItem.vnum + 1);
    }

    /* Mobiles */

    editMobile(vnum: number)
    {
        this.setCurrentMobile(vnum);
        this.goMobilesPanel();
    }

    setCurrentMobile(vnum: number): void
    {
        this.currentMobile = this.area.mobiles.get(vnum);
    }

    clearCurrentMobile(): void
    {
        this.currentMobile = null;
    }

    isNewMobileVnumDisabled(): boolean
    {
        return this.firstFreeMobileVnum < 1;
    }

    isCreateMobileDisabled(): boolean
    {
        if (this.userSetMobileVnum == undefined)
        {
            return this.firstFreeMobileVnum < 1;
        }

        if (this.userSetMobileVnum >= this.area.info.minVnum &&
            this.userSetMobileVnum <= this.area.info.maxVnum &&
            this.vnumService.isVnumFree(this.userSetMobileVnum, this.area.mobiles.keys()))
        {
            return false;
        }

        return true;
    }

    createMobile(): number
    {
        let mob = new Mobile();
        mob.vnum = this.userSetMobileVnum != undefined ? this.userSetMobileVnum : this.firstFreeMobileVnum;
        this.area.mobiles.set(mob.vnum, mob);

        this.setNextMobileVnum();

        return mob.vnum;
    }

    cloneMobile(vnum: number): void
    {
        let origin = this.area.mobiles.get(vnum);
        let clone = this.area.mobiles.get(this.createMobile());

        clone.maxPerWorld = origin.maxPerWorld;
        clone.keywords = origin.keywords;
        clone.gender = origin.gender;
        clone.name = new Name(origin.name);
        clone.look = origin.look;
        clone.description = origin.description;
    }

    setNextMobileVnum(): void
    {
        this.firstFreeMobileVnum = this.getFirstFreeVnum(this.area.mobiles.keys());
        this.userSetMobileVnum = this.firstFreeMobileVnum < 0 ? undefined : this.firstFreeMobileVnum;
    }

    isPrevMobileDisabled(): boolean
    {
        return !this.area.mobiles.has(this.currentMobile.vnum - 1);
    }

    isNextMobileDisabled(): boolean
    {
        return !this.area.mobiles.has(this.currentMobile.vnum + 1);
    }

    goPrevMobile(): void
    {
        this.currentMobile = this.area.mobiles.get(this.currentMobile.vnum - 1);
    }

    goNextMobile(): void
    {
        this.currentMobile = this.area.mobiles.get(this.currentMobile.vnum + 1);
    }

    /* Shared */

    setNextVnums(): void
    {
        this.setNextRoomVnum();
        this.setNextItemVnum();
        this.setNextMobileVnum();
    }

    private clearCurrentEntities(): void
    {
        this.clearCurrentRoom();
        this.clearCurrentItem();
        this.clearCurrentMobile();
    }

    private getFirstFreeVnum(vnums: IterableIterator<number>): number
    {
        return this.vnumService.getFirstFreeVnum(vnums, this.area.info.minVnum, this.area.info.maxVnum);
    }
}

enum Panel
{
    Info = 0,
    Rooms = 1,
    Items = 2,
    Mobiles = 3,
}
