/**
* Plugin Crystal Gallery
* 
* Copyright (C) 2012 Adamantium Solutions (www.adamantium.sk)
*
* @package     jquery.crystalGallery
* @author      Adamantium Solutions
* @copyright   2012 Adamantium Solutions
* @link        http://www.crystalgallery.adamantium.sk
*/

//TODO: change function main comments to be more descriptive

(function($){ 
    
    /**
    * this are the default options
    * 
    * @type Object
    */
    var defaults = {
        galleryLogo: "CRYSTAL<br />Gallery",    //logo can be text, image, any HTML content, leave empty or null for disabled logo
        layout: "fullscreen",                   //fullscreen, fixed
        navigation: "thumbs",                   //thumbs, none  //TODO build bullets style navigation
        navPosition: "bottom",                  //top, bottom, left, right
        flickrApiKey: null,                     //to use other than public photo stream set up your flickt api key - http://www.flickr.com/services/apps/create/apply
        keyboardNav: true,                      //allow gallery navigation using keyboard arrows
        thumbZoom: 2,                           //zoom thumbnail on hover, 0 for disabled zoom
        thumbsHidden: false,                    //are thumbnails hidden or not by default
        thumbsShowHandle: true,                 //show thumbnails mover handle if neccessary
        thumbsMouseMove: false,                 //move thumbnails on mouse over 
        uiAutoHide: false,                      //hide UI on mouse inactivity
        uiAutoHideDelay: 3,                     //seconds until UI autohides
        uiAutoHideThumbnails: true,             //also hide thumbnails automatically
        uiAutoHideDescriptions: false,          //also hide photo descriptions
        uiAnimationSpeed: 750,                  //speed of UI animations
        animationsEasing: "easeInOutExpo",      //easing type of animations
        gfxDir: null,                           //path to graphics directory
        externalImageLoader: null,              //path to script used for loading external images
        externalImageCache: true,               //use cache for external images
        autoPlay: true,                         //automatically show next photos
        autoplayTime: 10000,                    //autoplay delay in miliseconds
        autoPlayTimerPos: 1,                    //autoplay timer position (1 - oposite, 0 - normal)
        autoPlayTimerVisible: true,             //show or hide autoplay timer
        loop: "all",                            //category, all, none
        allowFullScreen: true,                  //fixed layout can be switched to fullscreen
        socialMediaEnabled: true,               //show social media icons
        socialAutoHide: true,                   //social media icons holder autohiding 
        socialAutoHideDelay: 5,                 //seconds until social media autohides
        socialMediaLinks: {                     //object containing social media links
            "facebook": "http://www.facebook.com/",
            "google": "http://plus.google.com/",
            "twitter": "http://twitter.com/",
            "flickr": "http://www.flickr.com/",
            "instagram": "http://web.stagram.com",
            "social-2": "http://plus.google.com/",
            "deviantart": "http://www.deviantart.com/",
            "500px": "http://500px.com"
        }, 
        showIcons: true,                        //wheather to show UI icons
        showPrevNext: true,                     //show previous, next controls
        showPhotoTitles: true,                  //show photo title if it is set
        showPhotoDesc: true,                    //show photo descriptions if set
        showPhotoDescAuto: true,                //show description automatically on photo change
        showPhotoDescClose: true,               //show description close button
        photoDescHorizontalMargin: 20,          //photo description margin from top or bottom 
        photoDescVerticalMargin: 20,            //photo description margin from left or right
        animatePhotoDescBy: 50,                 //show description animation
        showCategories: true,                   //show categories or not
        shufflePhotos: false,                   //shuffle photos order
        translucentStrength: 30,                //blur strength of translucent effect
        translucentOpacity: 0.3,                //opacity of translucent effect
        translucentColor: "#000000",            //default color of translucent UI - translucentColor can be changed for each category
        defaultThumbRows: 1,                    //default number of thumbnail rows - defaultThumbRows can be changed for each category
        defaultScale: "stretch-center",         //stretch, stretch-center, fit - defaultScale can be changed for each category
        defaultTransition: "slide",             //slide, slide-in, slide-out, fade - defaultTransition can be changed for each category
        defaultSpeed: 750,                      //speed of transition - defaultSpeed can be changed for each category,
        defaultDescAlign: "top-left",           //align of the description box - defaultDescAlign can be changed for each photo
        defaultDescWidth: 400                   //width of description box - defaultDescWidth can be changed for each photo (px or %)
    };
    
    
    
    
    /**
    * public methods
    * 
    * @type Object
    */              
    var publicMethods = {
          
        /**
        * main initialising function
        * 
        */
        init: function(opts) { 
            return this.each(function() {  
                //extend the default options
                this.opts = $.extend({}, defaults, opts);
                
                //correct easing
                if (!$.easing || !$.easing[this.opts.animationsEasing]) this.opts.animationsEasing = "swing";
                
                this.photos = new Array();
                this.categories = new Array();
                this.currentCategory = 0;
                
                //galery navigation is horizontal
                this.opts.navHorizontal = (this.opts.navPosition == "bottom" || this.opts.navPosition == "top");
                this.opts.defaultLayout = this.opts.layout;
                
                //get root of javascript file
                var root = null;
                $("script").each(function(){
                    if (this.src && /crystalGallery(|.min|.pack|.packed)?.js/.test(this.src)) {
                        root = this.src.substring(0, this.src.lastIndexOf('/') - 2); return;
                    }
                });   
                
                //set root for php and gfx dir
                if(this.opts.gfxDir == null) this.opts.gfxDir = root + 'gfx';
                if(this.opts.externalImageLoader == null) this.opts.externalImageLoader = '/visual/php/getImageData.php';
                
                //cursor position for content mover
                contentMover.defaults.gfx = this.opts.gfxDir;  
                
                //load categories and photo informations
                privateMethods._loadGalleryData.apply(this);
                
                //remove to speed up the gallery
                $(".crystal-category", this).remove();
                
                //at least one category is required
                if(this.categories.length < 1) {
                    alert("Please make sure that you have at least one category in your gallery");
                }
                
                //initiate animation queue
                this.internalAnimationQueue = new Array();
                this.internalAnimationQueueTimeout = null;
                
                //choose gallery layout
                privateMethods._buildHTML.apply(this);    
                
                //fire first resize event
                privateMethods._resizeEventHandler.apply(this);
                
                //fire first resize event
                privateMethods._bindGalleryEvents.apply(this);
                
                //load first category or open category chooser
                if(!this.usesFlickr) privateMethods._loadPhoto.apply(this, [0, 0]);
                
                return this;
            });    
        },
        
        
        
        /**
        * load photo from category
        * 
        */                        
        loadPhoto: function(category, photo){
            var $this = this;
            if(this[0] != null) var $this = this[0];
                
            //dont load if is allready loaded
            if($this.currentCategory == category && $this.currentPhoto == photo) return false;
            
            //preload photo and apply after load function
            if($this.categories[category] != null && $this.photos[category][photo] != null) {
                //select thumbnail
                privateMethods._selectThumbnail.apply($this, [category, photo]);
                
                //load photo
                privateMethods._loadPhoto.apply($this, [category, photo]);
                return $this.photos[category][photo];
            }
            
            return false;
        },
        
        
        
        /**
        * change category public method
        * 
        */               
        changeCategory: function(category){
            var $this = this;
            if(this[0] != null) var $this = this[0];
                
            //load first photo from category
            privateMethods._loadPhoto.apply(this, [category, 0]);
            return $this.photos[category][0]; 
        },
        
        
        
        /**
        * navigate to previous photo
        * 
        */
        previousPhoto: function() {
            var $this = this;
            if(this[0] != null) var $this = this[0];
                
            //load previous photo
            return privateMethods._prevPhoto.apply($this);
        },
        
        
        
        /**
        * navigate to next photo
        * 
        */
        nextPhoto: function() {
            var $this = this;
            if(this[0] != null) var $this = this[0]; 
                
            //load next photo
            return privateMethods._nextPhoto.apply($this);
        },
        
        
        
        /**
        * navigate to first photo
        * 
        */
        firstPhoto: function() {
            var $this = this;
            if(this[0] != null) var $this = this[0];
                
            //load first photo
            privateMethods._loadPhoto.apply($this, [0, 0]);
            return $this.photos[0][0]; 
        },
        
        
        
        /**
        * navigate to last photo
        * 
        */
        lastPhoto: function() {
            var $this = this;
            if(this[0] != null) var $this = this[0];
            
            //load last photo
            var lastCategory = $this.categories.length - 1;
            var lastPhoto = $this.photos[lastCategory].length - 1;
            privateMethods._loadPhoto.apply($this, [lastCategory, lastPhoto]);
            
            return $this.photos[lastCategory][lastPhoto];
        },
        
        
        
        /**
        * navigate to first photo in category
        * 
        */
        firstInCategory: function() {
            var $this = this;
            if(this[0] != null) var $this = this[0];
            
            //load first photo in category
            privateMethods._loadPhoto.apply($this, [$this.currentCategory, 0]);
            return $this.photos[$this.currentCategory][0];
        },
        
        
        
        /**
        * navigate to last photo in category
        * 
        */
        lastInCategory: function() {
            var $this = this;
            if(this[0] != null) var $this = this[0];
                
            //load last photo in category
            var lastCategory = $this.categories.length - 1;
            var lastPhoto = $this.photos[$this.currentCategory].length - 1;
            privateMethods._loadPhoto.apply($this, [$this.currentCategory, lastPhoto]);
            
            return $this.photos[$this.currentCategory][lastPhoto];
        },
        
        
        
        /**
        * get or set an option
        * 
        */
        option: function(option, value) {
            var $this = this;
            if($this[0] != null) var $this = this[0];
            
            if($this.opts.hasOwnProperty(option)) {
                //set option
                if(value) {
                    $this.opts[option] = value; 
                    return $this.opts[option];
                    
                //get option
                } else {
                    return $this.opts[option];
                }
            } else {
                $.error("Option " + option + " does not exist on jquery.friendChooser");
            }   
        }
    };
    
    
    
    
    /**
    * private methods
    * 
    * @type Object
    */               
    var privateMethods = {
        
        /**
        * load category and photo data
        * 
        */
        _loadGalleryData: function() {
            var $this = this;
            
            //get gallery categories 
            $(".crystal-category", this).hide().each(function(i) {
                var category = { };
                category.title = $(this).attr("title");
                
                //category description
                var desc = $(this).children(".crystal-category-desc");
                category.desc = desc ? desc.text() : "";
                
                //category settings
                var classList = $(this).attr('class').split(/\s+/);
                $(classList).each(function(index, item){
                    switch(true) {
                        //overide default transition for category
                        case item.indexOf("crystal-transition-") != -1:
                            category.transition = item.replace("crystal-transition-", "");
                            break;
                        
                        //overide default speed for category    
                        case item.indexOf("crystal-speed-") != -1:
                            var speed = parseInt(item.replace("crystal-speed-", ""));
                            category.speed = speed ? speed : $this.opts.defaultSpeed;
                            break;
                        
                        //overide default scale for category
                        case item.indexOf("crystal-scale-") != -1:
                            category.scale = item.replace("crystal-scale-", "");
                            break;
                        
                        //set category thumb size
                        case item.indexOf("crystal-thumb-size-") != -1:
                            var thumbSize = item.replace("crystal-thumb-size-", "").split("x");
                            if(thumbSize.length > 1){
                                category.thumbSize = {
                                    width: parseInt(thumbSize[0]),
                                    height: parseInt(thumbSize[1])
                                }    
                            }
                            break;
                        
                        //number of thumbnail rows
                        case item.indexOf("crystal-thumb-rows-") != -1:
                            category.thumbRows = parseInt(item.replace("crystal-thumb-rows-", ""));
                            break;
                        
                        //overide default color for category
                        case item.indexOf("crystal-color-") != -1: 
                            category.color = "#" + item.replace("crystal-color-", "");
                            break;
                    }  
                });

                //set default values for category
                if(category.transition == null) category.transition = $this.opts.defaultTransition;
                if(category.speed == null) category.speed = $this.opts.defaultSpeed;
                if(category.scale == null) category.scale = $this.opts.defaultScale;
                if(category.thumbRows == null || !category.thumbRows) category.thumbRows = $this.opts.defaultThumbRows;
                if(category.color == null) category.color = $this.opts.translucentColor;
                        
                //thumbnail size for category photos is required
                if(category.thumbSize == null) alert("Please set thumbanil size for category " + (category.title || (i + 1)));
                
                //get flickr photos
                var flickrPhotos = $(".crystal-flickr", this);
                if(flickrPhotos.length > 0) {
                    $this.usesFlickr = true;
                    $this.photos[i] = new Array();
                    privateMethods._getPhotosFromFlickr.apply($this, [flickrPhotos, i]);
                    
                } else {
                    //TODO: check if copying data to varibles is not slowing the script
                    //load category photos
                    $(".crystal-photo", this).each(function(j){
                        var photo = { };
                        photo.title = $(this).attr("title");
                        photo.nr = j + 1;
                        
                        //photo image
                        var img = $(this).children(".crystal-image");
                        if(img.length > 0) photo.imgSrc = img.attr("title");
                        
                        //photo medium
                        var medium = $(this).children(".crystal-medium");
                        if(medium.length > 0) photo.mediumSrc = medium.attr("title");
                        
                        //photo thumb
                        var thumb = $(this).children(".crystal-thumb");
                        if(thumb.length > 0) photo.thumbSrc = thumb.attr("title");
                        
                        //put this photo in array
                        var link = $(this).children("a.crystal-link");
                        if(link.length > 0) photo.link = link;
                        
                        //photo description
                        var desc = $(this).children(".crystal-desc");
                        if(desc.length > 0) {
                            photo.desc = desc.html();
                            
                            //get settings for photo description
                            var descClassList = desc.attr('class').split(/\s+/);
                            $(descClassList).each(function(index, item){
                                if(item.indexOf("crystal-align-") != -1) {
                                    photo.descAlign = item.replace("crystal-align-", "");    
                                    
                                } else if(item.indexOf("crystal-desc-width-") != -1) {
                                    photo.descWidth = item.replace("crystal-desc-width-", "");    
                                }
                            });
                            
                            //set default values for photo description
                            if(photo.descAlign == null) photo.descAlign = $this.opts.defaultDescAlign;
                            if(photo.descWidth == null) photo.descWidth = $this.opts.defaultDescWidth;
                        }
                        
                        //put this photo in array
                        if(photo.imgSrc) {
                            if($this.photos[i] == null) $this.photos[i] = new Array();
                            $this.photos[i].push(photo);    
                        }
                    });
                    
                    //shuffle photos
                    if($this.opts.shufflePhotos) $this.photos[i] = utilityMethods._shuffleArray($this.photos[i]);
                }
                
                //put this category in array
                $this.categories.push(category);
            });
        },
        
        
        
        /**
        * get photos from flickr
        *   
        */
        _getPhotosFromFlickr: function(flickrPhotos, i) {
            var $this = this;
            
            var limit = 20; //max limit for public feed is 20
            var lang = null;
            
            //get flickr feed settings
            var classList = flickrPhotos.attr('class').split(/\s+/);    
            $(classList).each(function(index, item){
                if(item.indexOf("crystal-flickr-limit-") != -1) {
                    limit = parseInt(item.replace("crystal-flickr-limit-", ""));
                }
                
                if(item.indexOf("crystal-flickr-lang-") != -1) {
                    lang = item.replace("crystal-flickr-lang-", "");
                } 
            });
            
            //get flickr source by class
            var flickrSource = $(".crystal-flickr-source", flickrPhotos);
            switch(true) {
                //flickr public set
                case flickrSource.hasClass("crystal-flickr-public-set"):
                    var url = flickrSource.attr("title") + "&format=json";
                    break;
                    
                //search flickr by text
                case flickrSource.hasClass("crystal-flickr-search"):
                    if(this.opts.flickrApiKey) {
                        var url = "http://api.flickr.com/services/rest/?format=json&method=flickr.photos.search&api_key=" + this.opts.flickrApiKey;
                        url += "&text=" + flickrSource.attr("title");
                    } else {
                        var url = "http://api.flickr.com/services/feeds/photos_public.gne?format=json";    
                        url += "&tags=" + flickrSource.attr("title");
                    }
                    if(lang) url += "&lang=" + lang;
                    break;
                
                //search flickr by tags
                case flickrSource.hasClass("crystal-flickr-tags"):
                    if(this.opts.flickrApiKey) {
                        var url = "http://api.flickr.com/services/rest/?format=json&method=flickr.photos.search&api_key=" + this.opts.flickrApiKey;
                    } else {
                        var url = "http://api.flickr.com/services/feeds/photos_public.gne?format=json";
                    }                      
                    url += "&tags=" + flickrSource.attr("title");
                    if(lang) url += "&lang=" + lang;
                    break;
                    
                //search flickr by photo set ID
                case flickrSource.hasClass("crystal-flickr-set"):
                    if(this.opts.flickrApiKey) {
                        var url = "http://api.flickr.com/services/rest/?format=json&method=flickr.photosets.getPhotos&api_key=" + this.opts.flickrApiKey;
                        url += "&photoset_id=" + flickrSource.attr("title");
                    }
                    break;
            }
            
            //vrong source specified error
            if(!url) {
                alert("Flickr feed source error: No vlaid source specified!");
                return false;
            }            
            
            //get feed from flickr
            $.getJSON(url + '&per_page=' + limit + '&jsoncallback=?', function(data){
                
                //show flickr error
                if(data.stat == "fail") {
                    alert(data.message);
                    return false;
                    
                //else fill gallery data
                } else {
                    $.each(data.items, function(j, item){
                        if(j < limit){ 
                            var photo = { };
                            
                            photo.title = item.title;
                            photo.nr = j + 1;
                            
                            //photo description
                            var desc = flickrPhotos.children(".crystal-desc");
                            if(desc.length > 0) {
                                photo.desc = item.description;
                                    
                                //get settings for photo description
                                var descClassList = desc.attr('class').split(/\s+/);
                                $(descClassList).each(function(index, item){
                                    if(item.indexOf("crystal-align-") != -1) {
                                        photo.descAlign = item.replace("crystal-align-", "");    
                                        
                                    } else if(item.indexOf("crystal-desc-width-") != -1) {
                                        photo.descWidth = item.replace("crystal-desc-width-", "");    
                                    }
                                });
                                
                                //set default values for photo description
                                if(photo.descAlign == null) photo.descAlign = $this.opts.defaultDescAlign;
                                if(photo.descWidth == null) photo.descWidth = $this.opts.defaultDescWidth;
                            }
                            
                            //diferent image sizes
                            item["square"] = item.media.m.replace("_m", "_s");      //75x75
                            item["thumbnail"] = item.media.m.replace("_m", "_s");   //100 on longest side
                            item["small"] = item.media.m;                           //240 on longest side
                            item["medium"] = item.media.m.replace("_m", "_z");      //640 on longest side
                            item["large"] = item.media.m.replace("_m", "_b");       //1024 on longest side 
                            
                            //photo image
                            var img = flickrPhotos.children(".crystal-image");
                            if(img.length > 0) photo.imgSrc = item[img.attr("title")];
                            
                            //photo thumb
                            var thumb = flickrPhotos.children(".crystal-thumb");
                            if(thumb.length > 0) photo.thumbSrc = item[thumb.attr("title")];
                            
                            //put this photo in array
                            if(photo.imgSrc) $this.photos[i].push(photo);    
                        }
                    }); 
                
                    //load gallery on flicker callback
                    if(!$this.flickerLoaded) {
                        $this.flickerLoaded = true;
                        privateMethods._loadPhoto.apply($this, [0, 0]);    
                    }
                    
                    delete data; 
                }
                //shuffle photos
                if($this.opts.shufflePhotos) $this.photos[i] = utilityMethods._shuffleArray($this.photos[i]);
            }); 
        },
        
                        
        
        /**
        * load previous photo
        * 
        */
        _prevPhoto: function() {
            //TODO: make code nonrepetitive with next photo
            //previous photo
            var prevPhoto = this.currentPhoto - 1;
            var prevCategory = this.currentCategory;
            
            if(prevPhoto < 0) {
                
                //category loop
                if(this.opts.loop == "category") {
                    prevPhoto = this.photos[this.currentCategory].length - 1;    
                    
                } else {
                    prevCategory = this.currentCategory - 1;
                    
                    //all loop
                    if (prevCategory < 0 && this.opts.loop == "all") {
                        prevCategory = this.categories.length - 1;
                        prevPhoto = this.photos[prevCategory].length - 1
                        
                    //no loop
                    } else if (prevCategory < 0) {
                        prevCategory = prevPhoto = 0;
                                
                    } else {
                        prevPhoto = this.photos[prevCategory].length - 1
                    }
                } 
            } 
            
            privateMethods._loadPhoto.apply(this, [prevCategory, prevPhoto, "prev"]);
            
            return this.photos[prevCategory][prevPhoto];
        },
        
        
        
        /**
        * load previous photo
        * 
        */
        _nextPhoto: function() {
            
            //next photo
            var nextPhoto = this.currentPhoto + 1;
            var nextCategory = this.currentCategory;
            
            if(nextPhoto > (this.photos[nextCategory].length - 1)) {
                
                //category loop
                if(this.opts.loop == "category") {
                    nextPhoto = 0;    
                    
                } else {
                    nextCategory = this.currentCategory + 1;
                    
                    //all loop
                    if (nextCategory > (this.categories.length - 1) && this.opts.loop == "all") {
                        nextCategory = nextPhoto = 0;
                    
                    //no loop    
                    } else if (nextCategory > (this.categories.length - 1)) {
                        nextCategory = (this.categories.length - 1);    
                        nextPhoto = this.photos[nextCategory].length - 1;
                            
                    } else {
                        nextPhoto = 0;
                    }
                } 
            } 
            
            privateMethods._loadPhoto.apply(this, [nextCategory, nextPhoto, "next"]);
            return this.photos[nextCategory][nextPhoto];
        }, 
        
        
        
        /**
        * show or hide navigation arrows
        * 
        */
        _resovleNavigationArrows: function(category, photo) {
            if(this.opts.loop != "category" && this.opts.loop != "all") {
                
                this.lastImage = false;
                this.firstImage = false;
                
                //hide next navigation button
                if(category >= (this.categories.length - 1) && photo >= (this.photos[category].length - 1)) {
                    this.html.next.fadeOut("slow");
                    this.html.prev.fadeIn("slow");
                    
                    this.lastImage = true;
                    
                //hide prev navigation button
                } else if(category <= 0 && photo <= 0) {
                    this.html.prev.fadeOut("slow");
                    this.html.next.fadeIn("slow");   
                    
                    this.firstImage = true;
                
                //show both navigation buttons
                } else {
                    this.html.prev.fadeIn("slow");
                    this.html.next.fadeIn("slow");   
                }
            }                                         
        },
        
           
        
        /**
        * change category with nice animation 
        * its a bit tricky for left and right thumbnails style
        * 
        */
        _changeCategory: function(category, photo, callback) {
            var $this = this;
            
            //check if gallery animations are bussy
            if(this.bussy) { utilityMethods._addToQueue.apply(this, ["_changeCategory", [category, photo, callback]]); return false; }
            this.bussy = true;
            
            var thisCategory = this.categories[category];
            var currentCategory = this.categories[this.currentCategory];
            var easing = this.opts.animationsEasing;
            
            //select category link
            $(".crystal-selected-category", this.html.Categories).removeClass("crystal-selected-category");
            $(".crystal-category-link", this.html.Categories).eq(category).addClass("crystal-selected-category");
            
            
            //thumbnails navigation
            if(this.opts.navigation == "thumbs") {
               
                //get width and height
                if(this.opts.navHorizontal) {
                    var width = thisCategory.thumbSize.width * Math.ceil(this.photos[category].length / thisCategory.thumbRows);
                    var height = thisCategory.thumbSize.height * thisCategory.thumbRows;
                } else {
                    var width = thisCategory.thumbSize.width * thisCategory.thumbRows;
                    var height = thisCategory.thumbSize.height * Math.ceil(this.photos[category].length / thisCategory.thumbRows);
                }                                                                                                                 
                
                var withCategories = this.categories.length > 1 && this.opts.showCategories;
                        
                //change height if thumbnails are visible
                if(this.html.Thumbnails.isVisible && this.currentPhoto != null) {
                     
                     //hide handle holder
                    this.galleryThumbnailsMover.handle.fadeOut(400, function(){
                        if(!$this.opts.navHorizontal) {
                            contentMover.resetMover($this.galleryThumbnailsMover);    
                            $(this).hide();   
                        }
                    });
                        
                    //horizontal thumbnails
                    if(this.opts.navHorizontal) {
                        
                        //hide element with fade animation
                        var elements = $(".crystal-thumbnail", this.html.Thumbnails).get().reverse();           
                        $.each(elements, function(i) { setTimeout(function(){ $(elements[i]).fadeOut(150); }, i * 40); });  
                        
                        //run callback after animations finishes
                        setTimeout(function(){
                            $this.html.Thumbnails.css({ height: "auto" });
                            
                            if($this.html.HandleHolder) {
                                //animate handle holder  
                                if(width > $this.html.HandleHolder.width()) {
                                    var adition = $this.html.HandleHolder.is(":visible") ? 0 : $this.html.HandleHolder.size;
                                    $this.html.HandleHolder.slideDown(300, easing);    
                                } else {
                                    var adition = $this.html.HandleHolder.is(":visible") ? -$this.html.HandleHolder.size : 0;
                                    $this.html.HandleHolder.slideUp(300, easing);
                                }
                            }
                            
                            $this.html.ThumbnailsContainer.stop(true, true)
                                .animate({ height: height }, 300, easing, function(){  
                                    
                                    //apply after change formating                                                                                         
                                    privateMethods._afterCategoryChange.apply($this, [category, photo, width, height, callback]);
                                });
                            
                            //resize UI holder div
                            var properties = { }; 
                            properties[$this.opts.navPosition] = "-=" + ($this.html.ThumbnailsContainer.height() - height - (adition || 0)); 
                            $this.html.UIHoder.animate(properties, 300, easing);
                                
                        }, elements.length * 50 + 250); 
                        
                    //vertical thumbnails
                    } else {
                     
                        var speed = this.opts.uiAnimationSpeed; 
                        
                        //animate thumbnails change
                        $(withCategories ? currentCategory.categoryThumbnails : this.html.ThumbnailsContainer)
                            .stop(true, true)
                            .animate({ height: 0 }, speed, easing, function(){   
                                var change = $this.html.ThumbnailsContainer.width() - width;
                                
                                //resize UI holder div
                                var properties = { }; properties[$this.opts.navPosition] = "-=" + change; 
                                $this.html.UIHoder.animate(properties, 300, easing);
                                    
                                //animate blurry background for right layout
                                if($this.opts.navPosition == "right") {
                                    $this.html.ThumbnailsBgImages
                                        .stop(true, true)
                                        .animate({ left: "-=" + change }, 300, easing);
                                }
                                
                                //vertical navigation correction for IE 7
                                if($.browser.msie && $.browser.version < 8 && !$this.opts.navHorizontal) {
                                    var paddingLeft = parseInt($this.html.ThumbnailsCategoriesHolder.css("paddingLeft"));
                                    var paddingRight = parseInt($this.html.ThumbnailsCategoriesHolder.css("paddingRight"));
                                    $this.html.ThumbnailsHolder.animate({ width : (width + paddingLeft + paddingRight) }, 300, easing);
                                }
                                 
                                //animate thumbnails
                                $this.html.ThumbnailsContainer.stop(true, true)
                                    .animate({ width: width }, 300, easing, function(){
                                        
                                        //apply after change formating
                                        privateMethods._afterCategoryChange.apply($this, [category, photo, width, height, callback]);
                                        
                                        //show new thumbnails in category
                                        $(withCategories ? thisCategory.categoryThumbnails : $this.html.ThumbnailsContainer)
                                            .css({ height: 0 })                                                             
                                            .animate({ height: height + $this.html.thumbnailsMargin }, speed, easing); 
                                    });   
                            });
                    }
                } else {
                    if(!this.opts.navHorizontal) {
                        //set thumbnails height
                        $(withCategories ? currentCategory.categoryThumbnails : this.html.ThumbnailsContainer).css({ height: 0 }); 
                        this.html.ThumbnailsContainer.css("width", width);
                    }
                    
                    //apply after change formating
                    privateMethods._afterCategoryChange.apply(this, [category, photo, width, height, callback]);            
                }
            } else { 
                //apply after change formating
                privateMethods._afterCategoryChange.apply(this, [category, photo, width, height, callback]);            
            }
            
            //change color of UI elements
            $([this.html.autoPlayTimer, this.html.ThumbnailsTranslucentColor, this.html.SocialBackground]).each(function(){
                $(this).css({ "background-color": $this.categories[category].color });
            });
        },
        
        
        
        /**
        * after change formating
        * 
        */                      
        _afterCategoryChange: function(category, photo, width, height, callback) {
            var $this = this;
            
            //thumbnail navigation
            if(this.opts.navigation == "thumbs") {
            
                var thisCategory = this.categories[category];
                var withCategories = this.categories.length > 1 && this.opts.showCategories;
                var dimensionProperty = this.opts.navHorizontal ? "height" : "width";
                var thumbnailsMargin = this.html.thumbnailsMargin || 0;
                
                //vertical navigation correctures for IE 7
                if($.browser.msie && $.browser.version < 8 && !this.opts.navHorizontal) {
                    var paddingLeft = parseInt(this.html.ThumbnailsCategoriesHolder.css("paddingLeft"));
                    var paddingRight = parseInt(this.html.ThumbnailsCategoriesHolder.css("paddingRight"));
                    this.html.ThumbnailsHolder.width(width + paddingLeft + paddingRight);
                }
                    
                //set width and height for categories holder
                if(this.opts.navHorizontal || !withCategories) {
                    this.html.ThumbnailsContainer 
                        .css({ width: width, height: (height + thumbnailsMargin) })
                        .empty(); 
                } else {
                    thisCategory.categoryThumbnails.css({ width: width, height: (height + thumbnailsMargin) });
                }                                                                                              
                
                if(!thisCategory.thumbnailsLoaded) {
                    
                    //load thumbnails for curent category
                    $(this.photos[category]).each(function(photo){
                        var thumbnail = privateMethods._buildThumbnailHTML.apply($this, [category, photo]);
                        
                        //append thumbnails
                        if($this.opts.navHorizontal || !withCategories) {
                            $this.html.ThumbnailsContainer.append(thumbnail);
                        } else {
                            if(photo < thisCategory.thumbRows) thumbnail.css({ marginTop: thumbnailsMargin });
                            thisCategory.categoryThumbnails.append(thumbnail);
                            thisCategory.thumbnailsLoaded = true;
                        }
                        
                        //show photos using fade animation
                        if($this.opts.navHorizontal) {
                            thumbnail.hide();   
                            setTimeout(function(){ thumbnail.fadeIn(150); }, photo * 40); 
                        } 
                    });   
                }                   
                
                //resize content custom mover
                contentMover.resetMover(this.galleryThumbnailsMover);    
                this.galleryThumbnailsMover.handle.hide().fadeTo(400, 0.5);
                
                //select thumbnail
                setTimeout(function(){ privateMethods._selectThumbnail.apply($this, [category, photo]); }, 200);
                
                //set social media holder width
                privateMethods._setSocialMediaWidth.apply(this, [width, height]);
            }   
            
            if(typeof callback == "function") callback.apply(this);
            this.bussy = false;
        },
        
        
        
        /**
        * set social media holder width or height
        * 
        */                                       
        _setSocialMediaWidth: function(width, height) {
            
            if(!this.opts.socialMediaEnabled) return false;
            var socialHeight = socialWidth = 0;
             
            //top, bottom layout
            if(this.opts.navHorizontal) {
                this.html.SocialHolder.children("a").each(function(){
                    socialHeight += $(this).outerHeight();
                    var width = $(this).outerWidth();
                    if(socialWidth < width) socialWidth = width;
                });
                
                if(socialHeight > height) socialWidth = socialWidth * 2;    
                this.html.SocialHolder.css({ width: socialWidth });
            
            //left and right layout
            } else {
                this.html.SocialHolder.children("a").each(function(){
                    socialWidth += $(this).outerWidth();
                    var height = $(this).outerHeight();
                    if(socialHeight < height) socialHeight = height;
                });
                
                if(socialWidth > width) socialHeight = socialHeight * 2;
                this.html.SocialHolder.css({ width: width, height: socialHeight });
            }
            
            if(this.html.SocialTrigger.hasClass("crystal-social-active")) {
                this.html.SocialContainer.css(
                    (this.opts.navHorizontal ? "width" : "height"), 
                    this.html.SocialHolder[this.opts.navHorizontal ? "outerWidth" : "outerHeight"]()
                );
                
                //resize content custom mover
                contentMover.resizeMover(this.galleryThumbnailsMover);
                privateMethods._resizeEventHandler.apply(this);
            }    
        },
        
        
        
        /**
        * change photo function
        * 
        */
        _changePhoto: function(direction) {
            
            //check if gallery animations are bussy
            if(this.bussy) { utilityMethods._addToQueue.apply(this, ["_changePhoto", [direction]]); return false; }
            this.bussy = true;   
            
            var category = this.categories[this.currentCategory];
            var speed = category.speed;
            
            
            //choose right transition
            switch(category.transition) {
                case "slide":
                    privateMethods._changePhotoSlide.apply(this, [direction, speed]);
                    break;
                    
                case "slide-in":
                    privateMethods._changePhotoSlideIn.apply(this, [direction, speed]);
                    break;
                    
                case "slide-out":
                    privateMethods._changePhotoSlideOut.apply(this, [direction, speed]);
                    break;
                    
                case "fade":
                    privateMethods._changePhotoFade.apply(this, [direction, speed]);
                    break;
            }
        },
        
        
        
        /**
        * sliding animation
        * 
        */
        _changePhotoSlide: function(direction, speed) {
            var $this = this;    
            
            var width = this.html.ImageCurrent.width();
            var directionFunc = (direction == "next") ? "append" : "prepend";
            
            this.html.ImageNext.show();
            
            if(this.opts.navigation == "thumbs") {
                this.html.ThumbnailsNext.show();
            
                //correct right thumbs animatiom
                if(this.html.ThumbnailsBgImages) var leftBefore = this.html.ThumbnailsBgImages.position().left;
                
                //run sliding animations
                this.html.ThumbnailsBgImages[directionFunc](this.html.ThumbnailsNext)
                    .css({ left: (direction == "next") ? leftBefore : (leftBefore - width) })
                    .stop(true, true)
                    .animate({ left: (direction == "next") ? (leftBefore - width) : leftBefore }, speed, this.opts.animationsEasing);            
            } 
            
            this.html.Images[directionFunc](this.html.ImageNext)
                .css({ left: (direction == "next") ? 0 : -width })
                .stop(true, true)
                .animate({ left: (direction == "next") ? -width : 0 }, speed, this.opts.animationsEasing, function(){
                    //after animation callback
                    privateMethods._afterChange.apply($this, [leftBefore || 0]);
                }); 
        },
        
        
        
        /**
        * slid-in animation
        * 
        */
        _changePhotoSlideIn: function(direction, speed) {
            var $this = this;    
            var width = this.html.ImageCurrent.width();
            
            this.html.ImageNext.show();
            
            if(this.opts.navigation == "thumbs") {
                this.html.ThumbnailsNext.show();
            
                //correct right thumbs animatiom
                if(this.html.ThumbnailsBgImages) var leftBefore = this.html.ThumbnailsBgImages.position().left;
                
                //run sliding animations
                this.html.ThumbnailsBgImages.append(this.html.ThumbnailsNext);
                
                this.html.ThumbnailsNext    
                    .css({ left: (direction == "next") ? width : -width, position: "absolute" })
                    .stop(true, true)
                    .animate({ left: 0 }, speed, this.opts.animationsEasing);            
            } 
            
            //slide in the next photo
            this.html.Images.append(this.html.ImageNext);
            this.html.ImageNext
                .css({ left: (direction == "next") ? width : -width, position: "absolute" })
                .stop(true, true)
                .animate({ left: 0 }, speed, this.opts.animationsEasing, function(){
                    //after animation callback
                    privateMethods._afterChange.apply($this, [leftBefore || 0]);
                }); 
        },
        
        
        
        /**
        * slide-out animation
        * 
        */
        _changePhotoSlideOut: function(direction, speed) {
            var $this = this;    
            var width = this.html.ImageCurrent.width();
            
            this.html.ImageNext.show();
            
            if(this.opts.navigation == "thumbs") {
                this.html.ThumbnailsNext.show();
            
                //correct right thumbs animatiom
                if(this.html.ThumbnailsBgImages) var leftBefore = this.html.ThumbnailsBgImages.position().left;
                
                //run sliding animations
                this.html.ThumbnailsBgImages.prepend(this.html.ThumbnailsNext);
                
                this.html.ThumbnailsCurrent
                    .css({ left: 0, position: "absolute" })
                    .stop(true, true)
                    .animate({ left: (direction == "next") ? -width : width }, speed, this.opts.animationsEasing);            
            } 
            
            //slide in the next photo
            this.html.Images.prepend(this.html.ImageNext);
            this.html.ImageCurrent
                .css({ left: 0, position: "absolute" })
                .stop(true, true)
                .animate({ left: (direction == "next") ? -width : width }, speed, this.opts.animationsEasing, function(){
                    //after animation callback
                    privateMethods._afterChange.apply($this, [leftBefore || 0]);
                });    
        },
        
        
        
        /**
        * face animation
        * 
        */
        _changePhotoFade: function(direction, speed) {
            var $this = this;    
            var width = this.html.ImageCurrent.width();
            
            this.html.ImageNext.show();
            
            if(this.opts.navigation == "thumbs") {
                this.html.ThumbnailsNext.show();
            
                //correct right thumbs animatiom
                if(this.html.ThumbnailsBgImages) var leftBefore = this.html.ThumbnailsBgImages.position().left;
                
                //run sliding animations
                this.html.ThumbnailsBgImages.append(this.html.ThumbnailsNext);
                
                this.html.ThumbnailsNext
                    .css({ left: 0, position: "absolute", opacity: 0 })
                    .stop(true, true)
                    .animate({ opacity: 1 }, speed, this.opts.animationsEasing);            
            } 
            
            //slide in the next photo
            this.html.Images.append(this.html.ImageNext);
            this.html.ImageNext
                .css({ left: 0, position: "absolute", opacity: 0  })
                .stop(true, true)
                .animate({ opacity: 1 }, speed, this.opts.animationsEasing, function(){
                    //after animation callback
                    privateMethods._afterChange.apply($this, [leftBefore || 0]);
                });    
        },
        
        
        
        /**
        * callback after animation has stopped
        * 
        */
        _afterChange: function(leftBefore) {
            var $this = this;
            this.bussy = false;    
            
            //clear images after animations
            this.html.ImageNext.css({ left: "", position: "" });
            this.html.Images.css({ left: 0 });
            this.html.ImageCurrent
                .empty()
                .css({ left: "", position: "" })
                .append(this.html.ImageNext.children());
            this.html.Images.append(this.html.ImageNext);  
            
            if(this.opts.navigation == "thumbs") {
                //clear blurry images after animations
                this.html.ThumbnailsNext.css({ left: "", position: "" });
                this.html.ThumbnailsBgImages.css({ left: leftBefore });
                this.html.ThumbnailsCurrent
                    .empty()
                    .css({ left: "", position: "" })
                    .append(this.html.ThumbnailsNext.children());
                this.html.ThumbnailsBgImages.append(this.html.ThumbnailsNext);
            }
            
            //if enabled show photo description automatically
            var curentPhoto = this.photos[this.currentCategory][this.currentPhoto];
            if(curentPhoto.desc && this.opts.showPhotoDesc && this.opts.showPhotoDescAuto && !this.opts.autoShowDisabled) {
                this.showPhotoDescriptionTimer = setTimeout(function(){
                    if(!$this.photoDescriptionActive) privateMethods._showPhotoDescription.apply($this, [curentPhoto]);    
                }, 500);
            }
        },
        
        
        
        /**
        * toggle loading animation
        * TODO: change to GIF to be faster
        */
        _toggleLoading: function(hide) {
            //hide loading animation    
            if(hide) {
                this.html.loading.removeClass("crystal-loader-visible"); 
                
            //show loading animation
            } else {
                this.html.loading.addClass("crystal-loader-visible");  
            }       
        },
        
        
        
        /**
        * start autoplay slideshow
        */
        _toggleAutoplay: function(start) {
            var $this = this;
            
            if(!this.html.autoPlayTimer) return false;
            var timerProperty = this.opts.navHorizontal ? "width" : "height";
            
            //star autoplay
            if(start || this.html.autoplayToggle.hasClass("crystal-autoplay-toggle-stopped")) {
                this.html.autoplayToggle.removeClass("crystal-autoplay-toggle-stopped");
                
                //hide autoplay timer on last photo
                if (this.lastImage) {
                    this.html.autoPlayTimer.stop(true, false).addClass("crystal-invisible"); 
                
                //reset autoplay timer position    
                } else {
                    this.html.autoPlayTimer.stop(true, false).css(timerProperty, 0).removeClass("crystal-invisible");
                }
                
                var properties = { }; properties[timerProperty] = "100%";
                
                //animate autoplay timer
                if(!this.html.autoPlayTimer.paused && !this.lastImage) {
                    this.html.autoPlayTimer.animate(properties, this.opts.autoplayTime, "linear", function() {
                        privateMethods._nextPhoto.apply($this);    
                    });
                }
            
            //stop autopley
            } else {
                this.html.autoplayToggle.addClass("crystal-autoplay-toggle-stopped");
                this.html.autoPlayTimer.stop(true, false).addClass("crystal-invisible");
            }
        },
        
        
        
        /**
        * resume autoplay slideshow
        */
        _continueAutoplay: function() {
            var $this = this;
            if(!this.html.autoPlayTimer) return false;
            if(this.html.autoplayToggle.hasClass("crystal-autoplay-toggle-stopped") || this.lastImage) return false;
            
            var timerProperty = this.opts.navHorizontal ? "width" : "height";
            var properties = { }; properties[timerProperty] = "100%";
            
            //get curent progress and time to end of autotimer
            var current = parseInt(this.html.autoPlayTimer[0].style[timerProperty]);
            var timeToEnd = this.opts.autoplayTime - (current / 100 * this.opts.autoplayTime);
            
            this.html.autoPlayTimer.animate(properties, timeToEnd, "linear", function() {
                privateMethods._nextPhoto.apply($this);    
            });   
        },
        
        
        
        /**
        * animate loading icon
        * 
        */
        _animateLoading: function(speed) {
            
            //get curent position
            var position = $(this.html.loading).css("background-position");
            if(typeof(position) === 'undefined') position = this.html.loading.css('background-position-x') + ' ' + this.html.loading.css('background-position-y');
            
            position = parseInt(position.split(" ")[0]) || 0;
            position = (position <= -1050 ? "0px" : "-=70") + " 0px";
            
            this.html.loading.css({ backgroundPosition: position });
        },
        
        
        
        /**
        * start loading photo
        * 
        */
        _loadPhoto: function(category, photo, force) {
            var $this = this;
            
            //dont load if is allready loaded
            if(this.currentCategory == category && this.currentPhoto == photo) return false;
            
            //one photo is already loading
            if(this.bussy) { utilityMethods._addToQueue.apply(this, ["_loadPhoto", [category, photo, force]]); return false; }
            this.bussy = true;
            
            var thisPhoto = this.photos[category][photo];
                
            //preload photo and apply after load function
            if(this.categories[category] != null && thisPhoto != null) {
                
                //show or hide navigation arrows
                privateMethods._resovleNavigationArrows.apply(this, [category, photo]);
                
                //stop animation timer
                if(this.html.autoPlayTimer) this.html.autoPlayTimer.stop(true, false);
                if(this.showPhotoDescriptionTimer != null) clearTimeout(this.showPhotoDescriptionTimer);
            
                //select thumbnail
                privateMethods._selectThumbnail.apply($this, [category, photo]);
                    
                //show loading 
                privateMethods._toggleLoading.apply(this, [false]);
                 
                //correct external images
                if(utilityMethods._isExternalURL(thisPhoto.imgSrc) && !($.browser.msie && $.browser.version < 9)) {
                    var useCache = this.opts.externalImageCache ? 1 : 0;
                    if(thisPhoto.imgSrc.indexOf("staticflickr.com") != -1) useCache = 0; //dont cache flicker photos
                    
                    thisPhoto.imgSrc = this.opts.externalImageLoader + "?url=" + thisPhoto.imgSrc + "&cache=" + useCache;
                } 
                 
                //image preloading 
                if(thisPhoto.image == null) thisPhoto.image = $("<img />").attr("src", thisPhoto.imgSrc);
                if(thisPhoto.blurry == null) thisPhoto.blurry = $("<img />").attr("src", thisPhoto.imgSrc);
                                 
                //blur photo
                thisPhoto.image.one('load', function(){
                    //callback function
                    var callback = function() {
                        thisPhoto.blurred = true;
                        
                        //after load callback
                        privateMethods._afterLoad.apply($this, [category, photo, force]); 
                    };
                    
                    //get natural width and height for IE
                    if($(this).prop("naturalWidth") == undefined) {
                        $(this).prop("naturalWidth", this.width);
                        $(this).prop("naturalHeight", this.height);
                    }
                    
                    if(!thisPhoto.blurred) privateMethods._blurImage.apply($this, [thisPhoto, callback]);
                    else callback();
                    
                }).each(function(){
                    if(this.complete || this.complete === undefined) this.src = this.src;        
                });
                
                //very wierd
                if(thisPhoto.blurred) thisPhoto.image.trigger("load");
            }
        },
        
        
        
        /**
        * make blurry image
        * 
        */ 
        _blurImage: function(thisPhoto, callback) {  
            
            //apply css blur filter for IE 7, 8
            if($.browser.msie && $.browser.version < 9) {
                if(this.opts.translucentStrength) {
                    thisPhoto.blurry.css({
                        "-ms-filter": "progid:DXImageTransform.Microsoft.Blur(pixelRadius=" + this.opts.translucentStrength + ")",
                        "filter": "progid:DXImageTransform.Microsoft.Blur(pixelRadius=" + this.opts.translucentStrength + ")"
                    });    
                }
                
                thisPhoto.blurred = true;
                if(callback != null) callback.apply(this);
            
            //apply stack blur for modern browsers
            } else {
                if(this.opts.translucentStrength) {
                    utilityMethods._stackBlurImage.apply(this, [thisPhoto.image, thisPhoto.blurry, this.opts.translucentStrength, callback]); 
                } else {
                    thisPhoto.blurry.attr("src", thisPhoto.image.attr("src"));
                    if(callback != null) callback.apply(this);   
                }
            }  
        },
        
        
        
        /**
        * what to do after image is preloaded
        * 
        */                                   
        _afterLoad: function(category, photo, force) {
            var $this = this;
            
            //get next photo and category                                             
            var nextCategory = this.categories[category];
            var nextPhoto = this.photos[category][photo];
            
            //show blurry for thumbnails
            if(nextPhoto.thumbsBgBlurry == null && this.opts.navigation == "thumbs") {  
                nextPhoto.thumbsBgBlurry = nextPhoto.blurry.clone().addClass("blurry-image");
            }
            
            //no category is loaded
            if(this.currentPhoto == null) { 
                nextPhoto.image.hide();
                if(nextPhoto.thumbsBgBlurry) nextPhoto.thumbsBgBlurry.hide();
                
                //insert into
                this.html.ImageCurrent.append(nextPhoto.image);
                if(this.html.ThumbnailsCurrent) this.html.ThumbnailsCurrent.append(nextPhoto.thumbsBgBlurry);
                
            } else {
                //insert into
                this.html.ImageNext.append(nextPhoto.image);
                if(this.html.ThumbnailsNext) this.html.ThumbnailsNext.append(nextPhoto.thumbsBgBlurry);
                
                //show next photo
                if(this.currentCategory < category || (this.currentCategory == category && this.currentPhoto < photo)) {
                    var direction = "next";
                
                //show prev photo
                } else if(this.currentCategory > category || (this.currentCategory == category && this.currentPhoto > photo)) {
                    var direction = "prev";
                }   
                
                if(force) var direction = force;
            } 
            
            //what to resize
            this.toResize = new Array();
            this.toResize.push(nextPhoto.image[0]);
            if(nextPhoto.thumbsBgBlurry) this.toResize.push(nextPhoto.thumbsBgBlurry[0]);
            
            //resize photo to fit or stretch on page
            privateMethods._resizePhoto.apply(this, [category, photo]); 
            
            //hide loading
            privateMethods._toggleLoading.apply(this, [true]);
            this.bussy = false;
            
            //run autoplay slideshow
            if(this.opts.autoPlay && this.html.autoplayToggle != null && !this.html.autoplayToggle.hasClass("crystal-autoplay-toggle-stopped")) {
                setTimeout(function() {
                    privateMethods._toggleAutoplay.apply($this, [true]);
                }, this.opts.uiAnimationSpeed);
            }
            
            //append photo link to UI holder
            this.html.UIHoder.children("a.crystal-link").remove();
            if(nextPhoto.link) this.html.UIHoder.append(nextPhoto.link);   
            
            //hide or show photo description icon if desc is not available
            if(this.html.photoDescriptionToggle) {
                var properties = { };
                properties[this.opts.navHorizontal ? "width" : "height"] = (nextPhoto.desc && this.opts.showPhotoDesc) ? "show" : "hide";
                
                this.html.photoDescriptionToggle.stop(true, true).animate(properties, this.opts.uiAnimationSpeed);
            } 
            
             
            //run animations
            if(this.currentPhoto != null) {
                
                //hide photo description if shown
                if(this.html.photoDescription) privateMethods._hidePhotoDescription.apply(this);
                
                //we are changing the category
                if(this.currentCategory != category) {                   
                    privateMethods._changeCategory.apply(this, [category, photo, function(){
                        privateMethods._changePhoto.apply($this, [direction]);                                                 
                        this.currentCategory = category;
                        this.currentPhoto = photo;
                    }]);
                } else {
                    privateMethods._changePhoto.apply($this, [direction]);                                                 
                    this.currentCategory = category;
                    this.currentPhoto = photo;
                } 
                
            } else {
                
                nextPhoto.image.fadeIn();
                if(nextPhoto.thumbsBgBlurry) nextPhoto.thumbsBgBlurry.fadeIn();    
                
                privateMethods._changeCategory.apply(this, [category, photo]);
                
                //show thumbnails holder
                if(this.opts.navigation == "thumbs" && !this.opts.thumbsHidden) privateMethods._toggleThumbnails.apply($this);    
                
                this.currentCategory = category;
                this.currentPhoto = photo;
                
                //if enabled show photo description automatically
                if(nextPhoto.desc && this.opts.showPhotoDesc && this.opts.showPhotoDescAuto) {
                    this.showPhotoDescriptionTimer = setTimeout(function(){
                        if(!$this.photoDescriptionActive) privateMethods._showPhotoDescription.apply($this, [nextPhoto]);      
                    }, 500);
                }
            }
        },
        
        
        
        /**
        * build photo description HTML and display it by animation
        *    
        */
        _showPhotoDescription: function(photo) {
            var $this = this;
            
            //photo description holder
            this.html.photoDescription = $("<div />").addClass("crystal-photo-description").width(photo.descWidth);
            
            var descContainer = $("<div />").addClass("crystal-photo-description-inner").html(photo.desc);
            var descEmboss = $("<div />").addClass("crystal-photo-description-emboss");
            var descBg = $("<div />").addClass("crystal-photo-description-bg").css({ 
                "background-color": this.categories[this.currentCategory].color
            });
            
            //blurry image
            this.html.photoDescriptionBlurry = photo.blurry.clone().addClass("blurry-description-bg");
            
            //first remove blurry element from toResize
            $(this.toResize).each(function(i){ if($(this).hasClass("blurry-description-bg")) { var where = i; return false; }});
            if(typeof where != "undefined") this.toResize.splice(where, 1);
            
            this.toResize.push(this.html.photoDescriptionBlurry[0]);
            
            //resize photo to fit or stretch on page
            privateMethods._resizePhoto.apply(this, [this.currentCategory, this.currentPhoto]);
            
            //append parts to description container
            this.html.photoDescription
                .append(this.html.photoDescriptionBlurry)
                .append(descBg)
                .append(descEmboss)
                .append(descContainer);
            
            //photo description close button
            if(this.opts.showPhotoDescClose) {
                var descClose = $("<div />").addClass("crystal-photo-description-close")
                    .bind("click", function(){
                        //hide photo
                        if($this.photoDescriptionActive) {
                            privateMethods._hidePhotoDescription.apply($this);    
                        }
                    });
                this.html.photoDescription.append(descClose);
            }
            
            this.html.UIHoder.append(this.html.photoDescription);
            
            //position photo description and animate it
            privateMethods._positionPhotoDescription.apply(this);
            privateMethods._animatePhotoDescription.apply(this);
        },
        
        
        
        /**
        * position photo description
        */
        _positionPhotoDescription: function() {
            
            var left = 0, top = 0, topBg = 0, leftBg = 0, iconsWidth = 0, iconsHeight = 0;
            var photo = this.photos[this.currentCategory][this.currentPhoto];
            var descAlign = photo.descAlign.split("-");
            
            if(!photo.desc || !this.opts.showPhotoDesc) return false;
            
            //get dimensions
            var width = this.html.photoDescription.width();
            var height = this.html.photoDescription.height();
            
            if(this.html.iconsHolder && this.opts.showIcons) {
                var iconsWidth = this.html.iconsHolder.outerWidth(); 
                var iconsHeight = this.html.iconsHolder.outerHeight();
            }
            
            var uiWidth = this.html.UIHoder.width();
            var uiHeight = this.html.UIHoder.height();
            
            //calculate vertical position
            switch(descAlign[0]) {
                case "center":
                    top = topBg = uiHeight / 2 - height / 2;    
                    if(this.opts.navPosition == "top") topBg = top + (this.galleryHeight - uiHeight);
                    break;
                    
                case "bottom":
                    top = topBg = uiHeight - height - this.opts.photoDescVerticalMargin;
                    
                    if(this.opts.navPosition == "bottom") top = topBg = top - iconsHeight;
                    if(this.opts.navPosition == "top") topBg = top + (this.galleryHeight - uiHeight);
                    break;
                    
                default:
                    top = topBg = this.opts.photoDescVerticalMargin;
                    if(this.opts.navPosition == "top") {
                        top = top + iconsHeight;
                        topBg = top + (this.galleryHeight - uiHeight);   
                    } 
            }
            
            //calculate horizontal position
            switch(descAlign[1]) {
                case "center":
                    left = leftBg = uiWidth / 2 - width / 2;
                    if(this.opts.navPosition == "left") leftBg = left + (this.galleryWidth - uiWidth);
                    break;
                    
                case "right":
                    var next = this.lastImage ? 0 : $(this.html.next).width();
                    left = leftBg = uiWidth - next - this.opts.photoDescHorizontalMargin - width;
                    
                    if(this.opts.navPosition == "right") left = leftBg = left - (next ? 0 : iconsWidth);
                    if(this.opts.navPosition == "left") leftBg = left + (this.galleryWidth - uiWidth);
                    break;
                    
                default:
                    var prev = this.firstImage ? 0 : $(this.html.prev).width();
                    left = leftBg = this.opts.photoDescHorizontalMargin + prev;
                    if(this.opts.navPosition == "left") {
                        left = left + (prev ? 0 : iconsWidth);
                        leftBg = left + (this.galleryWidth - uiWidth);
                    }
            }
            
            //first position the elements
            this.html.photoDescription.css({ top: top, left: left });
            this.html.photoDescriptionBlurry.css({ top: -topBg, left: -leftBg });
        },
        
        
        
        /**
        * animate photo description
        * 
        */
        _animatePhotoDescription: function(photo) {
            
            //show using animation
            this.html.photoDescription
                .css("left", "+=" + this.opts.animatePhotoDescBy)
                .stop(true, true)
                .animate({
                    opacity: 1, 
                    left: "-=" + this.opts.animatePhotoDescBy
                }, this.opts.uiAnimationSpeed, this.opts.animationsEasing);
            
            //animate blurry background
            this.html.photoDescriptionBlurry
                .css("left", "-=" + this.opts.animatePhotoDescBy)
                .stop(true, true)
                .animate({ 
                    left: "+=" + this.opts.animatePhotoDescBy 
                }, this.opts.uiAnimationSpeed, this.opts.animationsEasing);
               
            this.photoDescriptionActive = true;
        },
        
        
        
        /**
        * hide photo description
        *    
        */
        _hidePhotoDescription: function() {
            var $this = this;
                                
            //show using animation
            this.html.photoDescription.stop(true, true).animate({
                opacity: 0, 
                left: "-=" + this.opts.animatePhotoDescBy
            }, this.opts.uiAnimationSpeed, this.opts.animationsEasing, function(){ 
                $this.html.photoDescription.remove(); 
                $this.html.photoDescription = null; 
            });
            
            this.html.photoDescriptionBlurry.stop(true, true)
                .animate({ left: "+=" + this.opts.animatePhotoDescBy }, this.opts.uiAnimationSpeed, this.opts.animationsEasing);
                
            this.photoDescriptionActive = false;
        },
        
        
        
        /**
        * toggle photo description  
        */
        _togglePhotoDescription: function() {
            
            var curentPhoto = this.photos[this.currentCategory][this.currentPhoto];
            if(this.showPhotoDescriptionTimer != null) clearTimeout(this.showPhotoDescriptionTimer);
            
            //hide photo
            if(this.photoDescriptionActive) {
                privateMethods._hidePhotoDescription.apply(this); 
                this.opts.autoShowDisabled = true;
                
            //show photo
            } else if(curentPhoto.desc && this.opts.showPhotoDesc && !this.photoDescriptionActive) {
                privateMethods._showPhotoDescription.apply(this, [curentPhoto]);  
                this.opts.autoShowDisabled = false;  
            }      
        },
        
        
        
        /**
        * toggle fullscreen  
        *  
        */
        _toggleFullscreen: function() {
            
            //make gallery fixed
            if(this.fullscreenToggleActive) {
                if(this.opts.defaultLayout == "fixed") {
                    this.html.gallery
                        .removeClass("crystal-layout-fullscreen")
                        .addClass("crystal-layout-fixed"); 
                        
                    $(this).append(this.html.gallery);
                    $("body").css("overflow", "");
                    
                    this.opts.layout = "fixed";
                }
                
                this.html.allowFullScreenToggle.removeClass("crystal-fullscreen-active");
                this.fullscreenToggleActive = false;
                
                //cancel fullscreen mode
                if (document.cancelFullScreen) document.cancelFullScreen();
                else if (document.mozCancelFullScreen) document.mozCancelFullScreen();
                else if (document.webkitCancelFullScreen) document.webkitCancelFullScreen();
                
            //make gallery fullscreen
            } else {
                
                if(this.opts.defaultLayout == "fixed") {
                    this.html.gallery
                        .removeClass("crystal-layout-fixed")
                        .addClass("crystal-layout-fullscreen"); 
                      
                    $("body").append(this.html.gallery);
                    $("body").css("overflow", "hidden");
                        
                    this.opts.layout = "fullscreen";
                }
                
                this.html.allowFullScreenToggle.addClass("crystal-fullscreen-active");
                this.fullscreenToggleActive = true;
                
                var gallery = this.html.gallery[0];
                //big fullscreen mode
                if (gallery.requestFullScreen) gallery.requestFullScreen();
                else if (gallery.mozRequestFullScreen) gallery.mozRequestFullScreen();
                else if (gallery.webkitRequestFullScreen) gallery.webkitRequestFullScreen();
            } 
            
            //fire first resize event
            var $this = this;
            setTimeout(function(){ privateMethods._resizeEventHandler.apply($this); }, 250)
        },           
        
        
        
        /**
        * build html markup for gallery
        *     
        */
        _buildHTML: function() {
            var $this = this;
            
            //build the main gallery div
            this.html = { };
            this.html.gallery = $("<div />").addClass("crystal-holder crystal-layout-" + this.opts.layout);
            
            //build image holders
            this.html.Images = $("<div />").addClass("crystal-images");
            this.html.ImageCurrent = $("<div />").addClass("crystal-image crystal-current");
            this.html.ImageNext = $("<div />").addClass("crystal-image crystal-next");
            this.html.UIHoder = $("<div />").addClass("crystal-ui-holder crystal-ui-layout-" + this.opts.navPosition);
            
            //append divs to gallery
            this.html.Images
                .append(this.html.ImageCurrent)
                .append(this.html.ImageNext);
                
            this.html.gallery
                .append(this.html.Images)
                .append(this.html.UIHoder);
            
            //build markup for the gallery UI
            privateMethods._buildGalleryUIHTML.apply(this);      
            
            //build action icons for gallery
            privateMethods._buildGalleryIconsHTML.apply(this);      
            
            
            //build thumbnails markup    
            if(this.opts.navigation == "thumbs") privateMethods._buildThumbnailsHolderHTML.apply(this);      
            
            //append the gallery
            if(this.opts.layout == "fullscreen") $("body").css("overflow", "hidden").append(this.html.gallery);
            else $(this).append(this.html.gallery);
        },
        
        
        
        /**
        *  build HTML for arrow navigation 
        * 
        */
        _buildGalleryUIHTML: function() {
            var $this = this;
            
            //build arrow navigation
            if(this.opts.showPrevNext) privateMethods._buildNavigationArrows.apply(this);      
            
            //ajax loader   //TODO: check performance
            this.html.loading = $("<div />").addClass("crystal-loader");
            setInterval(function(){ privateMethods._animateLoading.apply($this); }, 50);
            
            this.html.UIHoder.append(this.html.loading);
            
            //build autoplay timer
            if(this.opts.autoPlay) {
                this.html.autoPlayTimer = $("<div />").addClass("crystal-autoplay-timer");
                if(this.opts.autoPlayTimerPos) this.html.autoPlayTimer.addClass("crystal-autoplay-timer-oposite");
                if(!this.opts.autoPlayTimerVisible) this.html.autoPlayTimer.hide();
                
                this.html.UIHoder.append(this.html.autoPlayTimer);
            }
        },
        
        
        
        /**
        * build action icons for gallery
        * 
        */
        _buildGalleryIconsHTML: function() {
            var $this = this;
            
            this.html.iconsHolder = $("<div />").addClass("crystal-icons-holder");
            if(!this.opts.showIcons) this.html.iconsHolder.hide();
             
            //icon for hiding and displaying thumbnails
            if(this.opts.navigation == "thumbs") {
                this.html.thumbnailsHider = $("<div />")
                    .addClass("crystal-thumbnail-toggle crystal-icon")
                    .bind("click", function(){ privateMethods._toggleThumbnails.apply($this); }); 
                    
                this.html.iconsHolder.append(this.html.thumbnailsHider);
            }
            
            //icon for autoplay option
            if(this.opts.autoPlay) {
                this.html.autoplayToggle = $("<div />")
                    .addClass("crystal-autoplay-toggle crystal-icon")
                    .bind("click", function(){ privateMethods._toggleAutoplay.apply($this); }); 
                    
                this.html.iconsHolder.append(this.html.autoplayToggle);
            }  
            
            //icon for show hide photo description
            if(this.opts.showPhotoDesc) {
                this.html.photoDescriptionToggle = $("<div />")
                    .addClass("crystal-description-toggle crystal-icon")
                    .bind("click", function(){ 
                        privateMethods._togglePhotoDescription.apply($this); 
                    }); 
                    
                this.html.iconsHolder.append(this.html.photoDescriptionToggle);
            }                                
            
            //icon for fullscreen / back to fullscreen
            if(this.opts.allowFullScreen) {
                this.html.allowFullScreenToggle = $("<div />")
                    .addClass("crystal-fullscreen-toggle crystal-icon")
                    .bind("click", function(){ 
                        privateMethods._toggleFullscreen.apply($this); 
                    }); 
                    
                this.html.iconsHolder.append(this.html.allowFullScreenToggle);
            }
            
            this.html.UIHoder.append(this.html.iconsHolder);
        },
        
        
        
        /**
        * build HTML for arrow navigation 
        * 
        */
        _buildNavigationArrows: function() {
            var $this = this;
            
            //navigation
            this.html.prev = $("<div />")
                .addClass("crystal-prev crystal-prev-next")
                .bind("click", function(){ privateMethods._prevPhoto.apply($this); })
                .append("<span />");
                
            this.html.next = $("<div />")
                .addClass("crystal-next crystal-prev-next")
                .bind("click", function(){ privateMethods._nextPhoto.apply($this); })
                .append("<span />");
            
            //append navigation to UI holder
            this.html.UIHoder
                .append(this.html.prev)
                .append(this.html.next);
            
            //hover arrow navigation
            /*if(this.opts.showPrevNextHover) {
                this.html.prevHover = $("<div />").addClass("crystal-prev-hover");
                this.html.nextHover = $("<div />").addClass("crystal-next-hover");   
            } */
        },
        
        
        
        /**
        * build HTML markup for thumbnails
        * 
        */                                
        _buildThumbnailsHolderHTML: function(){
            var $this = this;
            
            //thumbnails holder
            this.html.Thumbnails = $("<div />").addClass("crystal-thumbnails clear-after crystal-thumbnails-" + this.opts.navPosition);                
            this.html.Thumbnails.isVisible = false;
            
            //thumbnails blurry background holder
            this.html.ThumbnailsBg = $("<div />").addClass("crystal-thumbnailsbg");
            this.html.ThumbnailsBgImages = $("<div />").addClass("crystal-images");
                
            this.html.ThumbnailsCurrent = $("<div />").addClass("crystal-thumbnailbg crystal-thumbnailbg-current");
            this.html.ThumbnailsNext = $("<div />").addClass("crystal-thumbnailbg crystal-thumbnailbg-next");
             
            this.html.ThumbnailsHolder = $("<div />").addClass("crystal-thumbnails-holder").css("margin-" + this.opts.navPosition, -5000);
            this.html.UIHoder.removeClass("crystal-ui-fullsize");
                
            //graphical elements
            this.html.ThumbnailsTranslucentColor = $("<div />").addClass("crystal-translucent-color")
                .css({ 
                    opacity: this.opts.translucentOpacity,
                    "background-color": this.opts.translucentColor
                });
            
            //semitransparent border with its shadow
            var transparentBorder = $("<div />").addClass("crystal-transparent-border");
                                
            //append HTML markup
            this.html.ThumbnailsBgImages
                .append(this.html.ThumbnailsCurrent)
                .append(this.html.ThumbnailsNext);
                
            this.html.ThumbnailsBg
                .append(this.html.ThumbnailsBgImages)
                .append(this.html.ThumbnailsTranslucentColor)
                .append(transparentBorder);
             
            this.html.Thumbnails
                .append(this.html.ThumbnailsBg)
                .append(this.html.ThumbnailsHolder);
                
            //append main thumbnail div to gallery    
            this.html.gallery.append(this.html.Thumbnails);
            
            //categories markup
            this.html.ThumbnailsCategoriesHolder = $("<div />").addClass("crystal-thumbnails-categories");
            this.html.ThumbnailsCategories = $("<div />").addClass("crystal-thumbnails-overflow");
            this.html.ThumbnailsContainer = $("<div />").addClass("crystal-thumbnails-mover");
            
            //bind thumbnail onhover event to stop autoplay timer
            if(this.opts.autoPlay) {
                this.html.ThumbnailsCategories.hover(function(){
                    $this.html.autoPlayTimer.stop(true, false);
                    $this.html.autoPlayTimer.paused = true;
                    
                }, function(){
                    privateMethods._continueAutoplay.apply($this);
                    $this.html.autoPlayTimer.paused = false;
                });
            }
            
            //bind custom content mover
            if(this.opts.thumbsShowHandle) {
                this.html.HandleHolder = $("<div />").addClass("crystal-handle-holder crystal-handle-" + (this.opts.navHorizontal ? "horizontal" : "vertical"));   
            }
            
            this.galleryThumbnailsMover = contentMover.bindMover(
                this.html.ThumbnailsCategories, 
                this.html.ThumbnailsContainer, 
                this.html.HandleHolder, { 
                    axis: this.opts.navHorizontal ? "x" : "y",
                    mousemove: this.opts.thumbsMouseMove 
                });    
            
            //build categories and thumbnail container markup
            if(this.opts.navHorizontal) {
                privateMethods._buildThumbnailsHorizontalHTML.apply(this);
            } else {
                privateMethods._buildThumbnailsVerticalHTML.apply(this);
            }
            
            //build html markup for photo titles
            if(this.opts.showPhotoTitles) privateMethods._buildPhotoTitleHTML.apply(this);
            
            //build crystal gallery logo markup
            if(this.opts.galleryLogo != null && this.opts.galleryLogo != "") privateMethods._buildGalleryLogoHTML.apply(this);
            
            //build crystal gallery social media for thumbnails navigation type
            var c = 0; for(var p in this.opts.socialMediaLinks) if(this.opts.socialMediaLinks.hasOwnProperty(p)) c++;
            if(this.opts.socialMediaEnabled && c > 0) privateMethods._buildThumbnailsSocialHTML.apply(this, [c]);  
        },
        
        
        
        /**
        * html for horizontal thumbnails and categories
        * 
        */
        _buildThumbnailsHorizontalHTML: function(){
            var $this = this;   
            
            //categories holder div
            this.html.Categories = $("<div />").addClass("crystal-categories");
                
            //add categories to container
            if(this.categories.length > 1 && this.opts.showCategories) {       
                $(this.categories).each(function(i){
                    if(this.title) {
                        //build category link
                        this.categoryLink = $("<a />")
                            .addClass("crystal-category-link")
                            .attr({ href: "javascript: void(0)", title: this.desc})
                            .text(this.title)
                            .bind("click", function(){
                                if($this.currentCategory != i) privateMethods._loadPhoto.apply($this, [i, 0]);   
                            });
                        $this.html.Categories.append(this.categoryLink);
                    }
                });
                
                $(".crystal-category-link:last", this.html.Categories).addClass("crystal-category-link-last");
            }
                
            //resize categories on windows load
            $(window).load(function(){
                $this.html.Categories.css({ width: utilityMethods._getDimensionsFromHidden($this.html.Categories, "width") + 25 });    
            });     
                
            this.html.ThumbnailsCategories
                .append(this.opts.navPosition == "bottom" ? this.html.ThumbnailsContainer : this.html.Categories)
                .append(this.html.HandleHolder)
                .append(this.opts.navPosition == "bottom" ? this.html.Categories : this.html.ThumbnailsContainer);
            
            //bind categories mover
            this.galleryCategoriesMover = contentMover.bindMover(
                this.html.ThumbnailsCategories, 
                this.html.Categories, 
                null, { mousemove: true });                
                                
            //append holder in thumbnails bg
            this.html.ThumbnailsCategoriesHolder.append(this.html.ThumbnailsCategories);
            this.html.ThumbnailsHolder.append(this.html.ThumbnailsCategoriesHolder);
            
            //get size of handle holder
            $(document).ready(function(){
                if($this.html.HandleHolder) {
                    var marginTop = parseInt($this.html.HandleHolder.css("marginTop"));
                    var marginBottom = parseInt($this.html.HandleHolder.css("marginBottom"));
                    $this.html.HandleHolder.size = ($this.html.HandleHolder.outerHeight() + marginTop + marginBottom) || 0;  
                } 
            });             
        },
        
        
        
        /**
        * html for vertical thumbnails and categories
        * 
        */                                           
        _buildThumbnailsVerticalHTML: function(){
            var $this = this;   
            
            this.html.ThumbnailsCategories.append(this.html.ThumbnailsContainer);
            
            //add categories to container
            if(this.categories.length > 1 && this.opts.showCategories) {       
                $(this.categories).each(function(i){
                    //categories holder div
                    var galleryCategories = $("<div />").addClass("crystal-categories clear-after");
                    
                    if(this.title) {
                        //build category link
                        this.categoryLink = $("<a />")
                            .addClass("crystal-category-link")
                            .attr({ href: "javascript: void(0)", title: this.desc })
                            .text(this.title)
                            .bind("click touchend", function(){
                                if($this.currentCategory != i) privateMethods._loadPhoto.apply($this, [i, 0]);   
                            });
                            
                        galleryCategories.append(this.categoryLink);
                    }
                    
                    //category thumbnail holder
                    this.categoryThumbnails = $("<div />").addClass("crystal-category-thumbnails");
                    galleryCategories.append(this.categoryThumbnails);
                    
                    $this.html.ThumbnailsContainer.append(galleryCategories); 
                });                                                                                                        
                
                $(".crystal-categories:last", this.html.ThumbnailsContainer).css({ "margin-bottom": 0 });
            } 
             
            //append holder in thumbnails bg
            this.html.ThumbnailsCategoriesHolder
                .append(this.opts.navPosition == "right" ? this.html.ThumbnailsCategories : this.html.HandleHolder)
                .append(this.opts.navPosition == "right" ? this.html.HandleHolder : this.html.ThumbnailsCategories);                   
                
            this.html.ThumbnailsHolder.append(this.html.ThumbnailsCategoriesHolder);    
            
            //get margin of thumbnails holder
            var margin = $(".crystal-categories:first", this.html.ThumbnailsContainer).css("margin-bottom");
            this.html.thumbnailsMargin = parseInt(margin ? margin.replace("px", "") : 0);
            
            $(window).load(function() {
                //get margin of thumbnails holder
                var margin = $(".crystal-categories:first", $this.html.ThumbnailsContainer).css("margin-bottom");
                $this.html.thumbnailsMargin = parseInt(margin ? margin.replace("px", "") : 0);    
            });                                                                                                         
        },
        
        
        
        /**
        * build HTML markup for one thumbnail
        * 
        */
        _buildThumbnailHTML: function(category, photo) {
            var $this = this;
            
            var thisCategory = this.categories[category];
            var thisPhoto = this.photos[category][photo];
            
            //TODO: select thumbnail on mobile devices   
            //thumbnail holder
            thisPhoto.thumbnail = $("<div />").addClass("crystal-thumbnail")
                .css({
                    width: thisCategory.thumbSize.width,
                    height: thisCategory.thumbSize.height
                })
                .bind("click", function(){
                    privateMethods._loadPhoto.apply($this, [category, photo]);
                });
             
            
            //bind thumbnail hover events
            if(!("ontouchstart" in document.documentElement)) {
                thisPhoto.thumbnail
                    .bind("mouseenter", function(){
                        privateMethods._thumbnailMouseOver.apply($this, [this, thisPhoto]);
                    })
                    .bind("mouseleave", function(){
                        privateMethods._thumbnailMouseOut.apply($this, [thisPhoto]);
                    })
                    .bind("mousemove", function(e){
                        privateMethods._thumbnailMouseMove.apply($this, [e, this]);
                    });              
            }
            
            var thumbImgInactive = $("<img />")
                .addClass("crystal-thumbnail-inactive crystal-invisible")
                .attr("src", thisPhoto.thumbSrc);
                
            //active thumbnail image
            thisPhoto.thumbImgActive = $("<img />")
                .addClass("crystal-thumbnail-active crystal-invisible")
                .attr("src", thisPhoto.thumbSrc);
            
            var eventData = { 
                inactive: thumbImgInactive,
                image: thisPhoto.thumbImgActive, 
                width: thisCategory.thumbSize.width,
                height: thisCategory.thumbSize.height 
            };
            
            //make nocache copy of thumbnail
            var thumbNoCache = $("<img />")
                .attr("src", thisPhoto.thumbSrc + ($.browser.opera || $.browser.msie ? "?cache=" + Date.now() : ""))
                .bind('load', eventData, function(e){
                    
                    var thumbWidth = this.width;
                    var thumbHeight = this.height;
                    
                    var width = e.data.width;
                    var height = e.data.height;
                    
                    //stretch the thumbnail
                    if ((thumbWidth / width) < (thumbHeight / height)) {
                        var targetWidth  = width;
                        var targetHeight = thumbHeight * width / thumbWidth;
                    } else {
                        var targetWidth  = thumbWidth * height / thumbHeight;
                        var targetHeight = height;
                    } 
                    
                    //resize original thumbnail
                    e.data.image
                        .css({ 
                            width: targetWidth + ($this.opts.thumbZoom * 2),
                            height: targetHeight + ($this.opts.thumbZoom * 2),
                            "margin-top": -$this.opts.thumbZoom,
                            "margin-left": -$this.opts.thumbZoom
                        })
                        .removeClass("crystal-invisible");
                    
                    //resize original thumbnail
                    e.data.inactive
                        .css({ width: targetWidth, height: targetHeight })
                        .removeClass("crystal-invisible");
                    
                    $(this).remove();
                });
                
            
            //thumbnail emboss effect
            var thumbEmboss = $("<div />").addClass("crystal-thumbnail-emboss");
            if(this.opts.showPhotoTitles) thumbEmboss.addClass("crystal-with-title");
            
            //append graphics to thumbnail holder
            thisPhoto.thumbnail
                .append(thumbImgInactive)
                .append(thisPhoto.thumbImgActive)
                .append(thumbEmboss);
            
            return thisPhoto.thumbnail;
        },
        
        
        
        /**
        * html markup for photo titles
        * 
        */        
        _buildPhotoTitleHTML: function() {
            var $this = this;
            
            this.html.photoTitleContainer = $("<div />").addClass("crystal-title-container");        
            this.html.photoTitle = $("<div />").addClass("crystal-photo-title");
            this.html.photoPosition = $("<div />").addClass("crystal-photo-position");
            this.photoTitleTimeout = null;
            
            //append html to container
            this.html.photoTitleContainer
                .append(this.opts.navPosition == "top" ? this.html.photoPosition : this.html.photoTitle)
                .append(this.opts.navPosition == "top" ? this.html.photoTitle : this.html.photoPosition);
            
            this.html.ThumbnailsHolder.append(this.html.photoTitleContainer);    
            
            //get dimension from hidden files
            if(!this.opts.navHorizontal) {
                $(window).load(function(){
                    var holders = $("div", $this.html.photoTitleContainer);
                    holders.text("foobar");
                    
                    $this.html.photoTitle.hide();
                    $this.html.photoTitleContainer.minHeight = $this.html.photoTitleContainer.height();
                    
                    $this.html.photoTitle.show();
                    $this.html.photoTitleContainer.maxHeight = $this.html.photoTitleContainer.height();
                    
                    holders.text("");
                });
            }
        },
        
        
        
        /**
        * build markup for gallery logo
        * 
        */                             
        _buildGalleryLogoHTML: function(){
            
            //logo table holder
            var logoContents = $("<td />").html(this.opts.galleryLogo);
            if($(this.opts.galleryLogo).children("div").length < 1) logoContents.addClass("crystal-logo-padding");
            
            //transparent logo divider
            var logoDivider = $("<div />").addClass("crystal-logo-divider");
            this.html.logo = $("<table />").append($("<tr />").append(logoContents));
            
            //build logo holder
            this.html.logoHolder = $("<div />").addClass("crystal-logo")
                .append(this.html.logo)
                .append(logoDivider);  
           
            //append logo to thumbnails holder  
            this.html.ThumbnailsCategoriesHolder[this.opts.navHorizontal ? "prepend" : "before"](this.html.logoHolder);
        },
        
        
        
        /**
        * build crystal gallery social media for thumbnails
        * 
        */
        _buildThumbnailsSocialHTML: function() {
            var $this = this;
            
            //social trigger
            this.html.SocialTrigger = $("<div />").addClass("crystal-social-trigger").text("Social media");
            
            //social icon holder
            this.html.SocialContainer = $("<div />").addClass("crystal-social-container");
            this.html.SocialHolder = $("<div />").addClass("crystal-social-holder");
            
            //semitransparent background
            this.html.SocialBackground = $("<div />")
                .addClass("crystal-social-background")
                .css({ opacity: 0.3, "background-color": this.opts.translucentColor });
            
            //add social icons to container
            for(var p in this.opts.socialMediaLinks) {
                if(this.opts.socialMediaLinks.hasOwnProperty(p)) {
                    var socialLink = $("<a />")
                        .addClass("crystal-social-link crystal-social-link" + p)
                        .attr({ href: this.opts.socialMediaLinks[p], target: "_blank" })
                        .append($("<img />").attr("src", this.opts.gfxDir + "/social-" + p + ".png"));
                        
                    this.html.SocialHolder.append(socialLink);    
                }
            }
            
            //append social trigger
            $(this.opts.navHorizontal ? this.html.Thumbnails : this.html.SocialContainer).append(this.html.SocialTrigger);
            
            //append items to container    
            this.html.SocialContainer
                .append(this.html.SocialBackground)
                .append(this.html.SocialHolder);
            
            //append social to thumbnails
            this.html.ThumbnailsHolder.append(this.html.SocialContainer);   
            
            this.autohideSocialMediaTimer = null;           
            this.html.SocialTrigger.bind("click", function(){
                privateMethods._toggleSocial.apply($this);
            });             
            
            //pause social media autohider on hover
            if(this.opts.socialAutoHide) {
                $([this.html.SocialContainer, (this.opts.navHorizontal ? this.html.SocialTrigger : null)]).each(function(){
                    $(this).hover(function(){
                        if($this.autohideSocialMediaTimer != null) clearTimeout($this.autohideSocialMediaTimer);
                    }, function() {
                        if($this.html.SocialTrigger.hasClass("crystal-social-active")) {
                            if($this.autohideSocialMediaTimer != null) clearTimeout($this.autohideSocialMediaTimer);
                            $this.autohideSocialMediaTimer = setTimeout(function(){ privateMethods._toggleSocial.apply($this); }, $this.opts.socialAutoHideDelay * 1000);
                        }
                    });
                });
            }
        },
        
        
        
        /**
        * bind neccessary gallery events
        * 
        */
        _bindGalleryEvents: function() {
            var $this = this;
            this.mouseIsOut = true;
            
            //show hidden UI elements
            var showHidden = function() {
                if($this.uiAutoHidden) {
                    if($this.html.prev) $this.html.prev.fadeIn($this.opts.uiAnimationSpeed);
                    if($this.html.next) $this.html.next.fadeIn($this.opts.uiAnimationSpeed);
                    $this.html.iconsHolder.fadeIn($this.opts.uiAnimationSpeed);
                    
                    if($this.opts.navigation == "thumbs" && $this.opts.uiAutoHideThumbnails && !$this.opts.thumbsHiddenForced) privateMethods._toggleThumbnails.apply($this, [false, true]);
                    if($this.photoDescriptionActive && $this.opts.uiAutoHideDescriptions) $this.html.photoDescription.fadeIn($this.opts.uiAnimationSpeed);
                                                            
                    //fire first resize event
                    privateMethods._resizeEventHandler.apply($this);
                    $this.uiAutoHidden = false;
                }
            };   
            
            //arrow key navigation for gallery
            if(this.opts.keyboardNav) {
                $(document).bind("keydown", { element: this }, function(event) {
                    
                    //left key
                    if (event.keyCode == 37) { privateMethods._prevPhoto.apply(event.data.element); return false; }
                    //right key
                    if (event.keyCode == 39) { privateMethods._nextPhoto.apply(event.data.element); return false; }
                    //up key
                    if (event.keyCode == 38) { event.data.element.uiAutohideCounter = 0; showHidden(); privateMethods._toggleThumbnails.apply(event.data.element, [false]); return false; }
                    //down key
                    if (event.keyCode == 40) { event.data.element.uiAutohideCounter = 0; showHidden(); privateMethods._toggleThumbnails.apply(event.data.element, [true]); return false; }
                });
            }
            
            //autohide user interface when mouse inactivity is longer as uiAutoHideDelay
            if(this.opts.uiAutoHide) {
                this.uiAutohideCounter = 0;
                
                //reset inactivity timer on mokuse move
                this.html.gallery.bind("mousemove", { element: this }, function(event) {
                    event.data.element.uiAutohideCounter = 0;    
                    showHidden();
                }).bind("mouseenter", function() {
                    $this.mouseIsOut = false;
                }).bind("mouseleave", function() {
                    $this.mouseIsOut = true;
                });
                
                //check for inactivity every second
                setInterval(function(){ 
                    $this.uiAutohideCounter++;
                    
                    if(!$this.uiAutoHidden && $this.uiAutohideCounter > $this.opts.uiAutoHideDelay && ($this.opts.layout == "fullscreen" || ($this.opts.layout == "fixed" && $this.mouseIsOut))) {
                        if($this.html.prev) $this.html.prev.fadeOut($this.opts.uiAnimationSpeed);
                        if($this.html.next) $this.html.next.fadeOut($this.opts.uiAnimationSpeed);
                        $this.html.iconsHolder.fadeOut($this.opts.uiAnimationSpeed);
                        
                        if($this.opts.navigation == "thumbs" && $this.opts.uiAutoHideThumbnails) privateMethods._toggleThumbnails.apply($this, [true, true]);
                        if($this.photoDescriptionActive && $this.opts.uiAutoHideDescriptions) $this.html.photoDescription.fadeOut($this.opts.uiAnimationSpeed);
                        
                        $this.uiAutoHidden = true;
                    } 
                }, 1000);
            }  
            
            //bind window resize function
            $(window).bind("resize load", { element: this }, function(event) {
                privateMethods._resizeEventHandler.apply(event.data.element);
            });    
        },
        
        
        
        /**
        * thumbnail mouseover event handler
        * 
        */
        _thumbnailMouseOver: function(thumbnail, thisPhoto) {
            
            if(this.opts.showPhotoTitles) {
                
                //clear title hiding timeout
                if(this.photoTitleTimeout != null) clearTimeout(this.photoTitleTimeout);
                
                //show photo title
                if(thisPhoto.title != null) {
                    var height = this.html.photoTitleContainer.maxHeight;
                    this.html.photoTitle.html(thisPhoto.title);
                    
                //hide photo title
                } else {
                    var height = this.html.photoTitleContainer.minHeight;
                }
                
                this.html.photoTitle.stop(true, true)[thisPhoto.title != null ? "slideDown" : "slideUp"](250);
                
                //show photo count and position in set
                var currentCategory = this.photos[this.currentCategory];
                this.html.photoPosition.text(thisPhoto.nr + "/" + currentCategory.length);
                
                //get margin for title
                var axisDimension = this.opts.navHorizontal ? "height" : "width";
                var axisProperty = this.opts.navHorizontal ? "top" : "left";
                var pos = $(thumbnail).offset()[axisProperty];
                
                if(this.opts.navPosition == "left" || this.opts.navPosition == "top") {
                    var overMargin = this.html.Thumbnails[axisDimension]() - ($(thumbnail)[axisDimension]() + pos - this.html.gallery.position()[axisProperty]); 
                } else {
                    var overMargin = pos - this.html.Thumbnails.offset()[axisProperty]; 
                }
                
                //set styles for photo title
                this.html.photoTitleContainer
                    .addClass("crystal-hover")
                    .css("margin-" + this.opts.navPosition, -overMargin)
                    .css("margin-top", -(height / 2)); 
            } 
        },
        
        
        
        /**
        * thumbnail mouseout event handler
        * 
        */
        _thumbnailMouseOut: function(thisPhoto) {
            var $this = this;
            
            if(this.opts.showPhotoTitles) {
                
                //set title hiding timeout
                this.photoTitleTimeout = setTimeout(function() {
                    $this.html.photoTitleContainer
                        .removeClass("crystal-hover")
                        .css("margin-" + $this.opts.navPosition, "");     
                }, 50);  
            }     
        },
        
        
        
        /**
        * thumbnail mover mouseout event handler
        * 
        */
        _thumbnailMouseMove: function(e) {
            if(this.opts.showPhotoTitles) {
                var gallery = this.html.gallery;
                var axisProperty = this.opts.navHorizontal ? "left" : "top";
                
                var min = this.galleryThumbnailsMover.offset - gallery.position()[axisProperty];
                var max = this.galleryThumbnailsMover.offsetMax - gallery.position()[axisProperty];                                                   
                
                //diferences between horizontal and vertical thumbnails
                if(this.opts.navHorizontal) {
                    var mousePosition = e.pageX - gallery.position().left;    
                    var dimension = this.html.photoTitleContainer.width();
                    
                    if((mousePosition + dimension) > max) {
                        this.html.photoTitleContainer.addClass("crystal-right-align");
                        mousePosition = mousePosition - dimension; 
                        
                    } else {
                        this.html.photoTitleContainer.removeClass("crystal-right-align");
                    }
                    
                } else {
                    var mousePosition = e.pageY - gallery.position().top;    
                    var dimension = parseInt(this.html.photoTitleContainer.css("margin-top").replace("px", ""));
                    
                    if((mousePosition + dimension) < min) mousePosition = min - dimension;
                    else if((mousePosition - dimension) > max) mousePosition = max + dimension;
                }
                
                //set mouse position for title container
                this.html.photoTitleContainer.css(this.opts.navHorizontal ? "left" : "top", mousePosition);    
            }    
        },
        
           
                
        /**
        * select thumbnail
        * 
        */
        _selectThumbnail: function(category, photo){
            var thisPhoto = this.photos[category][photo];
            
            if(this.opts.navigation == "thumbs") {
                $("div.crystal-selected", this.html.ThumbnailsContainer).removeClass("crystal-selected");
                $(thisPhoto.thumbnail).addClass("crystal-selected");
            }
        },
        
        
        
        /**
        * toggle thumbnails chooser
        * 
        */
        _toggleThumbnails: function(hide, auto) {
            var $this = this;
            
            if(this.opts.navigation != "thumbs") return false;
            
            if(hide === true && !this.html.Thumbnails.isVisible) return false;
            if(hide === false && this.html.Thumbnails.isVisible) return false;
            
            //check if gallery animations are bussy
            if(this.bussy) { utilityMethods._addToQueue.apply(this, ["_toggleThumbnails", [hide, auto]]); return false; }
            this.bussy = true;
            
            //type of action and type of functions
            var elementType = this.opts.navHorizontal ? "ThumbnailsCategoriesHolder" : "ThumbnailsHolder";
            var classFunction = this.html.Thumbnails.isVisible ? "removeClass" : "addClass";
            var animateFunction = this.html.Thumbnails.isVisible ? "fadeOut" : "fadeIn";
            var dimensionProperty = this.opts.navHorizontal ? "height" : "width";
            
            //get values before animation
            var dimension = this.html.ThumbnailsHolder.css("margin-" + this.opts.navPosition, 0)[dimensionProperty]();
            this.html[elementType]
                .css("margin-" + this.opts.navPosition, this.html.Thumbnails.isVisible ? 0 : -dimension);
            
            var aminationStyles = { };
            aminationStyles["margin-" + this.opts.navPosition] = this.html.Thumbnails.isVisible ? -dimension : 0;                
            
            
            //animate thumbnail dimensions
            this.html[elementType].stop(true, true)
                .animate(aminationStyles, {
                    duration: this.opts.uiAnimationSpeed,
                    easing: this.opts.animationsEasing,
                    complete: function(){ 
                        $this.bussy = false;
                        if(!$this.html.Thumbnails.isVisible) $this.html.ThumbnailsHolder.css("margin-" + $this.opts.navPosition, -5000);
                    },
                    step: function(now){ 
                        var pos = dimension + now;
                        $this.html.UIHoder.css($this.opts.navPosition, pos - 1);
                        
                        //TODO: remove to speed up gallery
                        //resize photo description if it is displayed 
                        if($this.photoDescriptionActive) privateMethods._positionPhotoDescription.apply($this);    
                    }
                });
                
            //animate blurry background for right layout
            if (this.opts.navPosition == "right") {
                var where = this.galleryWidth - (this.html.Thumbnails.isVisible ? 0 : dimension);
                this.html.ThumbnailsBgImages.stop(true, true)
                    .animate({ left: -where }, this.opts.uiAnimationSpeed, this.opts.animationsEasing);
            }
            
            //show social trigger
            if(this.html.SocialTrigger != null) this.html.SocialTrigger[animateFunction]();
            
            //negate visible toggler
            if(!auto) this.opts.thumbsHiddenForced = this.html.Thumbnails.isVisible;
            this.html.Thumbnails.isVisible = !this.html.Thumbnails.isVisible;  
            
            //toggle icon class
            this.html.thumbnailsHider[classFunction]("crystal-thumbnail-toggle-open");
        },
        
        
        
        /**
        * toggle social icons
        * 
        */
        _toggleSocial: function() {
            var $this = this;
            
            //type of action    
            var show = !this.html.SocialTrigger.hasClass("crystal-social-active");
            
            var classFunc = show ? "addClass" : "removeClass";
            this.html.SocialTrigger[classFunc]("crystal-social-active");    
            
            //get function type for dimensions
            var dimension = this.html.SocialHolder[this.opts.navHorizontal ? "outerWidth" : "outerHeight"]();
            
            //animate social container
            var animateOptions = { };
            animateOptions[this.opts.navHorizontal ? "width" : "height"] = show ? dimension : 0;
            
            this.html.SocialContainer.stop(true, true)
                .animate(animateOptions, this.opts.uiAnimationSpeed, this.opts.animationsEasing)
                .css({ overflow: "visible" });
            
            //animate gallery width
            var animateOptions = { };
            
            if(this.opts.navHorizontal) animateOptions["marginRight"] = show ? dimension : 0;
            else animateOptions["height"] = (show ? "-=" : "+=") + dimension;
            
            $(this.opts.navHorizontal ? this.html.ThumbnailsCategories : this.html.ThumbnailsCategoriesHolder).stop(true, true)
                .animate(animateOptions, {
                    duration: this.opts.uiAnimationSpeed,
                    easing: this.opts.animationsEasing,
                    step: function() {
                        contentMover.resizeMover($this.galleryThumbnailsMover);    
                    }
                });
            
            //clear timeout
            if(this.autohideSocialMediaTimer != null && !show) clearTimeout(this.autohideSocialMediaTimer);   
        },
        
        
        
        /**
        * resize event handler
        * 
        */
        _resizeEventHandler: function() {
            
            //get sizes based on layout
            if(this.opts.layout == "fixed") {
                this.galleryWidth = $(this).width();    
                this.galleryHeight = $(this).height();  
                
            } else if(this.opts.layout == "fullscreen") {
                this.galleryWidth = $(window).width();    
                this.galleryHeight = $(window).height();
            }                  
            
            //resize the main div
            this.html.gallery.css({ 
                width: this.galleryWidth,
                height: this.galleryHeight
            });
                                                                                                                                                
            //resize image holders
            this.html.Images.css({ width: this.galleryWidth * 2 });
            $(".crystal-image", this.html.Images).css({ width: this.galleryWidth });
                                
            //thumbnails holder width     
            if(this.opts.navigation == "thumbs") {
                this.html.ThumbnailsBgImages.css({ width: this.galleryWidth * 2 });   
                $(".crystal-thumbnailbg", this.html.ThumbnailsBgImages).css({ width: this.galleryWidth });
                
                var logoDimension = 0;
                if(this.opts.galleryLogo != null && this.opts.galleryLogo != "") logoDimension = this.html.logoHolder[this.opts.navHorizontal ? "outerWidth" : "outerHeight"]();
                var socialDimension = this.opts.socialMediaEnabled ? this.html.SocialContainer[this.opts.navHorizontal ? "width" : "height"]() : 0;
                
                if(this.opts.navHorizontal) {   
                    this.html.ThumbnailsCategories.css({ marginLeft: logoDimension, marginRight: socialDimension });
                    
                //left, right layout
                } else {
                    var padding = this.html.ThumbnailsCategoriesHolder.outerHeight() - this.html.ThumbnailsCategoriesHolder.height() ;
                    this.html.ThumbnailsCategoriesHolder.css({ height: (this.galleryHeight - logoDimension - socialDimension - padding) });
                    
                    //resize content custom mover
                    contentMover.resizeMover(this.galleryThumbnailsMover);    
                }   
                
                //resize content custom mover
                if(this.opts.navHorizontal) contentMover.resizeMover(this.galleryCategoriesMover);
                contentMover.resizeMover(this.galleryThumbnailsMover);                            
                
                //resize UI holder div
                this.html.UIHoder.css(this.opts.navPosition, this.html.Thumbnails[this.opts.navHorizontal ? "height" : "width"]());
            }
            
            //resize photo description if it is displayed
            if(this.photoDescriptionActive) {
                privateMethods._positionPhotoDescription.apply(this);    
            }   
            
            //resize photo to fit or stretch on page
            privateMethods._resizePhoto.apply(this, [this.currentCategory, this.currentPhoto]);
        },
        
        
        
        /**
        * resize photo fit or stretch
        * 
        */
        _resizePhoto: function(category, photo) {
            var $this = this;
            
            var currentCategory = this.categories[category];
            var currentPhoto = this.photos[category][photo];
            
            //get category
            if(currentCategory && currentPhoto) {
                
                var photoWidth = currentPhoto.image[0].naturalWidth;
                var photoHeight = currentPhoto.image[0].naturalHeight;
                
                //stretch or fit the gallery
                if (((currentCategory.scale == "fit") && 
                    ((photoWidth / this.galleryWidth) > (photoHeight / this.galleryHeight))) || 
                    ((currentCategory.scale == "stretch" || currentCategory.scale == "stretch-center") && 
                    ((photoWidth / this.galleryWidth) < (photoHeight / this.galleryHeight)))) {
                        
                    var targetWidth  = this.galleryWidth;
                    var targetHeight = photoHeight * this.galleryWidth / photoWidth;
                } else {
                    var targetWidth  = photoWidth * this.galleryHeight / photoHeight;
                    var targetHeight = this.galleryHeight;
                }
                
                var marginTop = this.galleryHeight - targetHeight; 
                var marginLeft = this.galleryWidth - targetWidth; 
                
                $(this.toResize).each(function(){
                    
                    //resize blur in IE
                    var isBlurry = $(this).hasClass("blurry-image") || $(this).hasClass("blurry-description-bg");
                    var addition = ($.browser.msie && $.browser.version < 9 && isBlurry) ? $this.opts.translucentStrength : 0;
                    
                    $(this).css({
                        width: targetWidth,
                        height: targetHeight,
                        "margin-top": ((currentCategory.scale != "stretch" ? marginTop : 0) / 2) - addition,
                        "margin-left": ((currentCategory.scale != "stretch" ? marginLeft : 0) / 2) - addition
                    });
                    
                    //position blurry thumbnail background
                    if($this.opts.navigation == "thumbs" && $(this).hasClass("blurry-image")) {
                        privateMethods._positionBlurryThumbnails.apply($this, [currentCategory, this, ($this.opts.navPosition == "bottom" ? marginTop : marginLeft)]);
                    }
                });
            }
        },
        
        
        
        /**
        * position blurry thumbnail background
        * 
        */
        _positionBlurryThumbnails: function(currentCategory, image, marginChange) {
            var $this = this;
            
            if(!image) return false;
            
            //move different for other layouts
            if(this.opts.navPosition == "bottom"){
                var margin = parseFloat($(image).css("margin-top").replace("px", ""));
                
                //resize blur in IE
                var addition = ($.browser.msie && $.browser.version < 9) ? this.opts.translucentStrength * 2 : 0;
                
                var properties = { "margin-top": "" };
                if(currentCategory.scale != "stretch") properties["margin-bottom"] = margin + addition;    
                else properties["margin-bottom"] = marginChange + addition; 
                
                $(image).css(properties);     
                    
            } else if (this.opts.navPosition == "right") {
                var where = this.galleryWidth - $(this.html.Thumbnails).width();
                this.html.ThumbnailsBgImages.css({ left: -where });  
            }
        }
    };
    
    
    
    
    /**
    * utility methods
    * 
    * @type Object
    */               
    var utilityMethods = {
        
        /**
        * add new item to internal animation queue
        * 
        */
        _addToQueue: function(name, params) {
            var $this = this;
            
            var item = { functionName: name, functionParams: params };
            this.internalAnimationQueue.push(item);
            
            //start queue proicesing
            if(this.internalAnimationQueueTimeout != null) clearTimeout(this.internalAnimationQueueTimeout);
            this.internalAnimationQueueTimeout = setTimeout(function(){ utilityMethods._processQueue.apply($this); }, 250);
        },
        
        
        
        /**
        * process internal animation queue
        * 
        */
        _processQueue: function(){
            var $this = this;
            if(this.internalAnimationQueue.length > 0) {
                if(!this.bussy) {
                    var item = this.internalAnimationQueue.shift();
                    privateMethods[item.functionName].apply($this, item.functionParams || []);
                    
                    if(this.internalAnimationQueue.length > 0) {
                        this.internalAnimationQueueTimeout = setTimeout(function(){ utilityMethods._processQueue.apply($this); }, 250);
                    } else if(this.internalAnimationQueueTimeout != null) {
                        clearTimeout(this.internalAnimationQueueTimeout);   
                    }
                } else {
                    this.internalAnimationQueueTimeout = setTimeout(function(){ utilityMethods._processQueue.apply($this); }, 250);
                }
            } else if(this.internalAnimationQueueTimeout != null) {
                clearTimeout(this.internalAnimationQueueTimeout);   
            }   
        },
        
        
        
        /**
        * process internal animation queue
        * 
        */
        _clearQueue: function(){
            this.internalAnimationQueue = new Array();
            if(this.internalAnimationQueueTimeout != null) clearTimeout(this.internalAnimationQueueTimeout);   
        },
        
        
        
        /**
        * shuffles an array
        *                  
        */
        _shuffleArray: function(array) {
            var len = array.length;
            var i = len;
            while (i--) {
                var p = parseInt(Math.random() * len);
                var t = array[i];
                array[i] = array[p];
                array[p] = t;
            }
            
            return array;    
        }, 
        
        
        
        /**
        * return dimensions of hidden elements
        * 
        */
        _getDimensionsFromHidden: function(element, type){
            var dimension = 0;
            
            if(element != null) {
                var tmp = $(element).clone();
                $("body")
                    .addClass("crystal-holder")
                    .append($(tmp)
                    .css({ position: "absolute", top: -5000, bottom: "" })
                    .show());
            
                if(type == "width") dimension = $(tmp).width();
                else if(type == "height") dimension = $(tmp).height();
                else if(type == "outerWidth") dimension = $(tmp).outerWidth();
                else if(type == "outerHeight") dimension = $(tmp).outerHeight();
                
                $(tmp).remove();
                $("body").removeClass("crystal-holder");
            }
            return dimension;
        },
        
        
        
        /**
        * check if given UEL is external
        * 
        */
        _isExternalURL: function (url) {
            var match = url.match(/^([^:\/?#]+:)?(?:\/\/([^\/?#]*))?([^?#]+)?(\?[^#]*)?(#.*)?/);
            if (typeof match[1] === "string" && match[1].length > 0 && match[1].toLowerCase() !== location.protocol) return true;
            if (typeof match[2] === "string" && match[2].length > 0 && match[2].replace(new RegExp(":("+{"http:":80,"https:":443}[location.protocol]+")?$"), "") !== location.host) return true;
            return false;
        },
        
        
        
        /*
        StackBlur - a fast almost Gaussian Blur For Canvas

        Version:    0.5
        Author:     Mario Klingemann
        Contact:    mario@quasimondo.com
        Website:    http://www.quasimondo.com/StackBlurForCanvas
        Twitter:    @quasimondo

        In case you find this class useful - especially in commercial projects -
        I am not totally unhappy for a small donation to my PayPal account
        mario@quasimondo.de

        Or support me on flattr: 
        https://flattr.com/thing/72791/StackBlur-a-fast-almost-Gaussian-Blur-Effect-for-CanvasJavascript

        Copyright (c) 2010 Mario Klingemann

        Permission is hereby granted, free of charge, to any person
        obtaining a copy of this software and associated documentation
        files (the "Software"), to deal in the Software without
        restriction, including without limitation the rights to use,
        copy, modify, merge, publish, distribute, sublicense, and/or sell
        copies of the Software, and to permit persons to whom the
        Software is furnished to do so, subject to the following
        conditions:

        The above copyright notice and this permission notice shall be
        included in all copies or substantial portions of the Software.

        THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
        EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
        OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
        NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
        HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
        WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
        FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
        OTHER DEALINGS IN THE SOFTWARE.
        */
        _stackBlurImage: function(a, b, c, d) {
            var e = $(a)[0];
            var b = $(b)[0];
            var f = e.width;
            var g = e.height;
            var h = $("<canvas />");
            h = h[0];
            h.style.width = f + "px";
            h.style.height = g + "px";
            h.width = f;
            h.height = g;
            var i = h.getContext("2d");
            i.clearRect(0, 0, f, g);
            i.drawImage(e, 0, 0, f, g);
            if (isNaN(c) || c < 1) return;
            try {
                stackBlurCanvasRGB.apply(this, [h, 0, 0, f, g, c, b, d]);
            } catch (k) {
                throw new Error("Unable to access image data.");
            }
            
            function stackBlurCanvasRGB(a, b, c, d, e, f, g, h) {
                if (isNaN(f) || f < 1) return;
                f |= 0;
                
                var mul_table = [512, 512, 456, 512, 328, 456, 335, 512, 405, 328, 271, 456, 388, 335, 292, 512, 454, 405, 364, 328, 298, 271, 496, 456, 420, 388, 360, 335, 312, 292, 273, 512, 482, 454, 428, 405, 383, 364, 345, 328, 312, 298, 284, 271, 259, 496, 475, 456, 437, 420, 404, 388, 374, 360, 347, 335, 323, 312, 302, 292, 282, 273, 265, 512, 497, 482, 468, 454, 441, 428, 417, 405, 394, 383, 373, 364, 354, 345, 337, 328, 320, 312, 305, 298, 291, 284, 278, 271, 265, 259, 507, 496, 485, 475, 465, 456, 446, 437, 428, 420, 412, 404, 396, 388, 381, 374, 367, 360, 354, 347, 341, 335, 329, 323, 318, 312, 307, 302, 297, 292, 287, 282, 278, 273, 269, 265, 261, 512, 505, 497, 489, 482, 475, 468, 461, 454, 447, 441, 435, 428, 422, 417, 411, 405, 399, 394, 389, 383, 378, 373, 368, 364, 359, 354, 350, 345, 341, 337, 332, 328, 324, 320, 316, 312, 309, 305, 301, 298, 294, 291, 287, 284, 281, 278, 274, 271, 268, 265, 262, 259, 257, 507, 501, 496, 491, 485, 480, 475, 470, 465, 460, 456, 451, 446, 442, 437, 433, 428, 424, 420, 416, 412, 408, 404, 400, 396, 392, 388, 385, 381, 377, 374, 370, 367, 363, 360, 357, 354, 350, 347, 344, 341, 338, 335, 332, 329, 326, 323, 320, 318, 315, 312, 310, 307, 304, 302, 299, 297, 294, 292, 289, 287, 285, 282, 280, 278, 275, 273, 271, 269, 267, 265, 263, 261, 259];
                var shg_table = [9, 11, 12, 13, 13, 14, 14, 15, 15, 15, 15, 16, 16, 16, 16, 17, 17, 17, 17, 17, 17, 17, 18, 18, 18, 18, 18, 18, 18, 18, 18, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24];
                
                var i = a.getContext("2d");
                var j;
                j = i.getImageData(b, c, d, e);
                var k = j.data;
                var l, m, n, o, p, q, r, s, t, u, v, w, x, y, z, A, B, C, D, E;
                var F = f + f + 1;
                var G = d << 2;
                var H = d - 1;
                var I = e - 1;
                var J = f + 1;
                var K = J * (J + 1) / 2;
                var L = new BlurStack;
                var M = L;
                for (n = 1; n < F; n++) {
                    M = M.next = new BlurStack;
                    if (n == J) var N = M
                }
                M.next = L;
                var O = null;
                var P = null;
                r = q = 0;
                var Q = mul_table[f];
                var R = shg_table[f];
                for (m = 0; m < e; m++) {
                    y = z = A = s = t = u = 0;
                    v = J * (B = k[q]);
                    w = J * (C = k[q + 1]);
                    x = J * (D = k[q + 2]);
                    s += K * B;
                    t += K * C;
                    u += K * D;
                    M = L;
                    for (n = 0; n < J; n++) {
                        M.r = B;
                        M.g = C;
                        M.b = D;
                        M = M.next
                    }
                    for (n = 1; n < J; n++) {
                        o = q + ((H < n ? H : n) << 2);
                        s += (M.r = B = k[o]) * (E = J - n);
                        t += (M.g = C = k[o + 1]) * E;
                        u += (M.b = D = k[o + 2]) * E;
                        y += B;
                        z += C;
                        A += D;
                        M = M.next
                    }
                    O = L;
                    P = N;
                    for (l = 0; l < d; l++) {
                        k[q] = s * Q >> R;
                        k[q + 1] = t * Q >> R;
                        k[q + 2] = u * Q >> R;
                        s -= v;
                        t -= w;
                        u -= x;
                        v -= O.r;
                        w -= O.g;
                        x -= O.b;
                        o = r + ((o = l + f + 1) < H ? o : H) << 2;
                        y += O.r = k[o];
                        z += O.g = k[o + 1];
                        A += O.b = k[o + 2];
                        s += y;
                        t += z;
                        u += A;
                        O = O.next;
                        v += B = P.r;
                        w += C = P.g;
                        x += D = P.b;
                        y -= B;
                        z -= C;
                        A -= D;
                        P = P.next;
                        q += 4
                    }
                    r += d
                }
                for (l = 0; l < d; l++) {
                    z = A = y = t = u = s = 0;
                    q = l << 2;
                    v = J * (B = k[q]);
                    w = J * (C = k[q + 1]);
                    x = J * (D = k[q + 2]);
                    s += K * B;
                    t += K * C;
                    u += K * D;
                    M = L;
                    for (n = 0; n < J; n++) {
                        M.r = B;
                        M.g = C;
                        M.b = D;
                        M = M.next
                    }
                    p = d;
                    for (n = 1; n <= f; n++) {
                        q = p + l << 2;
                        s += (M.r = B = k[q]) * (E = J - n);
                        t += (M.g = C = k[q + 1]) * E;
                        u += (M.b = D = k[q + 2]) * E;
                        y += B;
                        z += C;
                        A += D;
                        M = M.next;
                        if (n < I) {
                            p += d
                        }
                    }
                    q = l;
                    O = L;
                    P = N;
                    for (m = 0; m < e; m++) {
                        o = q << 2;
                        k[o] = s * Q >> R;
                        k[o + 1] = t * Q >> R;
                        k[o + 2] = u * Q >> R;
                        s -= v;
                        t -= w;
                        u -= x;
                        v -= O.r;
                        w -= O.g;
                        x -= O.b;
                        o = l + ((o = m + J) < I ? o : I) * d << 2;
                        s += y += O.r = k[o];
                        t += z += O.g = k[o + 1];
                        u += A += O.b = k[o + 2];
                        O = O.next;
                        v += B = P.r;
                        w += C = P.g;
                        x += D = P.b;
                        y -= B;
                        z -= C;
                        A -= D;
                        P = P.next;
                        q += d
                    }
                }
                i.putImageData(j, b, c);
                g.src = a.toDataURL();
                
                delete i;
                delete a;
                delete j;
                delete k;
                
                a = null;
                j = null;
                k = null;
                i = null;
                
                var $this = this;
                //$(g).one("load", function () {
                    if (h != null) setTimeout(function () {
                        h.apply($this)
                    }, 250)
                //})
            }
            function BlurStack() {
                this.r = 0;
                this.g = 0;
                this.b = 0;
                this.a = 0;
                this.next = null
            }
        }
    };
    
    
    
    
    /**
    * Plugin Adaptive Custom Scrollbar
    * 
    * Copyright (C) 2012  Adamantium Solutions (www.adamantium.sk)
    *
    * @package     jquery.adaptiveCustomScrollbar
    * @author      Adamantium Solutions
    * @copyright   2012 Adamantium Solutions
    * @link        http://www.adamantium.sk
    */
    var contentMover = {
        defaults: {
            wheel: true, 
            wheelSpeed: 200,
            drag: true, 
            easing: 7, 
            axis: "x",
            mousemove: false,
            gfx: ""
        },
        
        //custom content mover
        bindMover: function(holder, content, handleHolder, options) {
            
            var scrollbar = $(content)[0]; 
            if(scrollbar == null) return null;
            
            scrollbar.options = $.extend({}, contentMover.defaults, options);
            
            //on mousemove disable drag
            if(scrollbar.options.mousemove === true) {
                scrollbar.options.wheel = false;   
                scrollbar.options.drag = false;
            }       
            
            //utility variables
            scrollbar.holder = $(holder);
            scrollbar.handleHolder = $(handleHolder);
            
            scrollbar.handle = $("<div />").addClass("crystal-mover-handle");
            scrollbar.focusor = $("<div />").addClass("crystal-mover-focusor").css({ opacity: 0, cursor: contentMover._resolveDragCursor(true, false) });     
            
            scrollbar.options.defaultEasing = scrollbar.options.easing;
            scrollbar.canDrag = false;
            scrollbar.movable = true;                    
            scrollbar.currentPosition = scrollbar.to = 0;
            
            //assign correct property and function
            scrollbar.axisProperty = (scrollbar.options.axis == "y") ? "top" : "left";
            scrollbar.axisFunc = (scrollbar.options.axis == "y") ? "height" : "width";
            scrollbar.axisPage = (scrollbar.options.axis == "y") ? "pageY" : "pageX";
            
            if(handleHolder != null) {
                scrollbar.options.handle = true;
                
                //append handle to holder
                scrollbar.handleHolder.append(scrollbar.handle);
                scrollbar.handle.css({ cursor: contentMover._resolveDragCursor(false, false) });     
            }
            
            //bind events handle
            if(scrollbar.options.handle) {
                scrollbar.handle.bind("mousedown click", { scrollbar: scrollbar }, contentMover._startHandleDrag);
                scrollbar.handleHolder.bind("click", { scrollbar: scrollbar }, contentMover._moverHandleClicked);
            }
            
            //use mouswheel to navigation
            if(scrollbar.options.wheel !== false) {
                $(scrollbar).bind('mousewheel', { scrollbar: scrollbar }, contentMover._moverMouseWheel);
            }
            
            //use draging to navigate
            if(scrollbar.options.drag !== false || "ontouchstart" in document.documentElement) {
                $(scrollbar).bind("mousedown touchstart", { scrollbar: scrollbar }, contentMover._startMoverDrag);
            }
            
            //use mousemove to navigate
            if(scrollbar.options.mousemove === true) {
                $(scrollbar).bind("mousemove", { scrollbar: scrollbar }, contentMover._mouseOverMove);
            }
            
            contentMover.resetMover(scrollbar);
            return scrollbar;
        }, 
        
        //get dimensions of slider
        resizeMover: function(scrollbar) {
            if(scrollbar == null) return false;
            
            //clear timeout on resize
            if(scrollbar.timeout != null) clearTimeout(scrollbar.timeout);
                
            //get height of scrollbar handle
            var scrollbarHandleSize = scrollbar.holder[scrollbar.axisFunc]() / $(scrollbar)[scrollbar.axisFunc]() * scrollbar.holder[scrollbar.axisFunc]();
            
            //remove handle is content is too smaller than holder
            if(scrollbar.holder[scrollbar.axisFunc]() >= $(scrollbar)[scrollbar.axisFunc]()) {
                scrollbar.handle.hide();
                scrollbar.handleHolder.hide();
                scrollbar.canDrag = false;
            } else {
                scrollbar.handle.show();
                scrollbar.handleHolder.show();
                scrollbar.canDrag = true;                                                                
                scrollbar.timeout = setTimeout(function() { contentMover._moveContent(scrollbar); }, 25);
            }
            
            if(scrollbarHandleSize < 40) scrollbarHandleSize = 40;
            scrollbar.handle.css(scrollbar.axisFunc, scrollbarHandleSize);
            
            //get scrollbar maximum and offset
            scrollbar.offset = scrollbar.holder.offset()[scrollbar.axisProperty];
            scrollbar.offsetMax = scrollbar.offset + scrollbar.holder[scrollbar.axisFunc]();
            scrollbar.handleMax = scrollbar.handleHolder[scrollbar.axisFunc]() - scrollbarHandleSize;
            scrollbar.max = $(scrollbar)[scrollbar.axisFunc]() - scrollbar.holder[scrollbar.axisFunc]();
            
            contentMover.correctScrollbarPosition(scrollbar);
        },
        
        //reset mover position
        resetMover: function(scrollbar) {
            if(scrollbar == null) return false;
            scrollbar.currentPosition = scrollbar.to = 0;
            
            //clear timeout on reset
            if(scrollbar.timeout != null) clearTimeout(scrollbar.timeout);
            scrollbar.timeout = setTimeout(function() { contentMover._moveContent(scrollbar); }, 25);
            
            $(scrollbar).css(scrollbar.axisProperty, scrollbar.to);
            scrollbar.handle.css(scrollbar.axisProperty, scrollbar.to);
            
            contentMover.resizeMover(scrollbar); 
        },
        
        //corect scroller and handle position on resize
        correctScrollbarPosition: function(scrollbar){
            if(scrollbar == null || !scrollbar.canDrag) return false;
            
            //check if handle is not out of holder
            var currentHandle = parseFloat(scrollbar.handle.css(scrollbar.axisProperty));
            
            //check if scroller is not out of holder
            var current = parseFloat($(scrollbar).css(scrollbar.axisProperty));
            if(!isNaN(current) && (current < -scrollbar.max)) {
                $(scrollbar).css(scrollbar.axisProperty, -scrollbar.max );
                scrollbar.to = scrollbar.currentPosition = -scrollbar.max;
            }
            
            //align handle
            if(!isNaN(currentHandle) && currentHandle > scrollbar.handleMax) contentMover.alignHandle(scrollbar, -scrollbar.max);   
        },
        
        //align handle (usefull for resizing)
        alignHandle: function(scrollbar, to) {
            if(!scrollbar.handle.is(":visible")) return false;
            
            var where = (scrollbar.handleMax * (-to / scrollbar.max));
            scrollbar.handle.css(scrollbar.axisProperty, where);
        },
        
        //chenge where to by content mover position
        setWhereByContent: function(scrollbar, where, over) {
            if(scrollbar == null) return false;
            
            if(where > 0) var correctedWhere = 0;
            else if(where < -scrollbar.max) var correctedWhere = -scrollbar.max;
            else var correctedWhere = where;
            
            contentMover.alignHandle(scrollbar, correctedWhere);                                                  
            
            if(over) {
                if(where > 0) var correctedWhere = Math.pow(where, 0.2) * 30 - 30;   
                else if(where < -scrollbar.max) var correctedWhere = -scrollbar.max -(Math.pow(Math.abs(where + scrollbar.max), 0.2) * 30 - 30);
            }
             
            scrollbar.to = correctedWhere;
        },
        
        //chenge where to animate
        setWhereByHandle: function(scrollbar, where) {
            if(scrollbar == null) return false;
            
            if(where < 0) where = 0;
            else if(where > scrollbar.handleMax) where = scrollbar.handleMax;
            
            scrollbar.to = - (scrollbar.max * (where / scrollbar.handleMax));
            scrollbar.handle.css(scrollbar.axisProperty, where);
        },
        
        //content scrolling animation
        _moveContent: function(scrollbar) { 
            scrollbar.currentPosition += (scrollbar.to - scrollbar.currentPosition) / scrollbar.options.easing;
                
            if(Math.round(scrollbar.currentPosition) != scrollbar.to) $(scrollbar).css(scrollbar.axisProperty, scrollbar.currentPosition);
            else $(scrollbar).css(scrollbar.axisProperty, scrollbar.to);
              
            scrollbar.timeout = setTimeout(function() { contentMover._moveContent(scrollbar); }, 25);    
        },
        
        //mousewheel event
        _moverMouseWheel: function(e, d) {  
            var scrollbar = e.data.scrollbar;
            scrollbar.options.easing = scrollbar.options.defaultEasing;
            
            var dir = d > 0 ? 1 : -1;
            
            var pos = scrollbar.to + (dir * scrollbar.options.wheelSpeed);
            contentMover.setWhereByContent(scrollbar, pos);
            
            e.preventDefault(); e.stopPropagation();   
        },
        
        //click handle event
        _moverHandleClicked: function(e) {
            var scrollbar = e.data.scrollbar;
            scrollbar.options.easing = scrollbar.options.defaultEasing;
            
            var where = e[scrollbar.axisPage] - scrollbar.offset;
            contentMover.setWhereByHandle(scrollbar, where);
        },
        
        //start handle draging
        _startHandleDrag: function(e) { 
            if(e.type == "mousedown") { 
                var scrollbar = e.data.scrollbar;
                if(scrollbar.canDrag === false) return;
                
                if(scrollbar.holder.find(scrollbar.focusor).length) scrollbar.focusor.remove();
                
                //bind events
                $(document).bind("mouseup", { scrollbar: scrollbar }, contentMover._stopHandleDrag);
                $(document).bind("mousemove", { scrollbar: scrollbar }, contentMover._moveHandleDrag);
                
                //start draging
                if(scrollbar != null) {
                    scrollbar.dragging = true;
                    scrollbar.options.easing = scrollbar.options.defaultEasing;
                    
                    scrollbar.handle.addClass("draging");
                    
                    //get positions
                    scrollbar.where = e[scrollbar.axisPage];
                    scrollbar.start = scrollbar.handle.position()[scrollbar.axisProperty];
                    
                    contentMover._resolveDragCursor(scrollbar.dragging, true);
                }
            }
            e.preventDefault(); e.stopPropagation();
        },
        
        //stop handle draging
        _stopHandleDrag: function(e) {
            var scrollbar = e.data.scrollbar;
            
            //unbind events
            $(document).unbind("mouseup", contentMover._stopHandleDrag);
            $(document).unbind("mousemove", contentMover._moveHandleDrag);
            
            scrollbar.handle.removeClass("draging");
            if(scrollbar.holder.find(scrollbar.focusor).length) scrollbar.focusor.remove();
            
            scrollbar.dragging = false;  
            contentMover._resolveDragCursor(scrollbar.dragging, true);
            
            e.preventDefault(); e.stopPropagation();
        },
        
        //draging the handle
        _moveHandleDrag: function(e) {
            var scrollbar = e.data.scrollbar;
            
            if(scrollbar.dragging && scrollbar.dragging != null) {
                if(scrollbar != null) {
                    
                    if(!scrollbar.holder.find(scrollbar.focusor).length) scrollbar.holder.append(scrollbar.focusor);
                    
                    //set where to move by handle
                    var where = e[scrollbar.axisPage] - scrollbar.where + scrollbar.start;
                    contentMover.setWhereByHandle(scrollbar, where);
                }
            }
            
            e.preventDefault(); e.stopPropagation();
        },
        
        //start handle draging
        _startMoverDrag: function(e) {
            var scrollbar = e.data.scrollbar;
            if(scrollbar.canDrag === false) return;
            
            //translate touch events
            var touches = e.originalEvent.changedTouches;
            if(touches != null && touches[0][scrollbar.axisPage]) e[scrollbar.axisPage] = touches[0][scrollbar.axisPage];
            
            if(scrollbar.holder.find(scrollbar.focusor).length) scrollbar.focusor.remove();
            
            //bind events
            $(document).bind("mouseup touchend", { scrollbar: scrollbar }, contentMover._stopMoverDrag);
            $(document).bind("mousemove touchmove", { scrollbar: scrollbar }, contentMover._moveMoverDrag);
            
            //start draging
            if(scrollbar != null) {
                scrollbar.dragging = true;
                scrollbar.movable = false;
                scrollbar.options.easing = scrollbar.options.defaultEasing;
                 
                //get position of drag start 
                scrollbar.where = e[scrollbar.axisPage];
                scrollbar.start = $(scrollbar).position()[scrollbar.axisProperty];
            } 
            e.preventDefault(); e.stopPropagation();
        },
        
        //stop handle draging
        _stopMoverDrag: function(e) { 
            var scrollbar = e.data.scrollbar;
            if(scrollbar.holder.find(scrollbar.focusor).length) scrollbar.focusor.remove();
            
            //translate touch events
            var touches = e.originalEvent.changedTouches;
            if(touches != null && touches[0][scrollbar.axisPage]) e[scrollbar.axisPage] = touches[0][scrollbar.axisPage];
            
            //unbind events
            $(document).unbind("mouseup touchend", contentMover._stopMoverDrag);
            $(document).unbind("mousemove touchmove", contentMover._moveMoverDrag);
                                      
            scrollbar.handle.removeClass("draging");
            
            var where = e[scrollbar.axisPage] - scrollbar.where + scrollbar.start;  
            contentMover.setWhereByContent(scrollbar, where, false);
            
            scrollbar.dragging = false;  
            scrollbar.movable = true;
            contentMover._resolveDragCursor(scrollbar.dragging, true);
            
            e.preventDefault(); e.stopPropagation();
        },
        
        //draging the handle
        _moveMoverDrag: function(e) {
            var scrollbar = e.data.scrollbar; 
            contentMover._resolveDragCursor(scrollbar.dragging, true);
            
            //translate touch events
            var touches = e.originalEvent.changedTouches;
            if(touches != null && touches[0][scrollbar.axisPage]) e[scrollbar.axisPage] = touches[0][scrollbar.axisPage];
            
            //append focusor
            if(!scrollbar.holder.find(scrollbar.focusor).length) {
                setTimeout(function(){
                    if(scrollbar.dragging && scrollbar.dragging != null) {
                        scrollbar.holder.append(scrollbar.focusor);   
                        scrollbar.handle.addClass("draging");
                    }                                           
                }, 100);
            }
            
            //drag the mover
            if(scrollbar.dragging && scrollbar.dragging != null) {
                if(scrollbar != null) {
                    //set where to move
                    var where = e[scrollbar.axisPage] - scrollbar.where + scrollbar.start;  
                    contentMover.setWhereByContent(scrollbar, where, true);
                }
            }
            
            e.preventDefault(); e.stopPropagation();
        },
        
        //move content on mouseover
        _mouseOverMove: function(e) {
            var scrollbar = e.data.scrollbar; 
            if(scrollbar.canDrag === false) return; 
             
            if(scrollbar != null) {
                scrollbar.options.easing = 10;                                                                                      
                
                //set where to move
                var where = -(scrollbar.max * ((e[scrollbar.axisPage] - scrollbar.offset - 75) / (scrollbar.offsetMax - scrollbar.offset - 150)));
                contentMover.setWhereByContent(scrollbar, where, false);
            }
        },
        
        //drag cursor for multiple browsers by Jordan Clist - http://www.jaycodesign.co.nz/css/cross-browser-css-grab-cursors-for-dragging/
        _resolveDragCursor: function(dragging, apply){
            var dragCursor;
            
            // IE doesn't support co-ordinates
            var cursCoords = $.browser.msie ? "" : " 4 4";
             
            if (dragging) {
                dragCursor = $.browser.mozilla ? "-moz-grabbing" : "url('" + contentMover.defaults.gfx + "/cursor-closedhand.cur')" + cursCoords + ", move";
                if ($.browser.opera) dragCursor = "move";
            } else if(!apply) {
                dragCursor = $.browser.mozilla ? "-moz-grab" : "url('" + contentMover.defaults.gfx + "/cursor-openhand.cur')" + cursCoords + ", move";
                if ($.browser.opera) dragCursor = "move";
            } else {
                dragCursor = "";
            }
            
            if(apply) $("body").css({ "cursor": dragCursor });
            else return dragCursor;   
        }
    };
          
    
    
    //plugin inicialization     
    $.fn.crystalGallery = function(method) {
        // Method calling logic
        if (publicMethods[method]) {
            return publicMethods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === 'object' || !method) {
            return publicMethods.init.apply(this, arguments);
        } else {
            $.error('Method ' +  method + ' does not exist on jquery.crystalGallery');
        } 
    }; 
          
})(jQuery);