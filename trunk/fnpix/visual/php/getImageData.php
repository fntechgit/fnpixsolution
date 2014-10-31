<?
/**
* Plugin Crystal Gallery
* 
* This script is used to get an image from external link and return it as local image.
* It is neccessary to get HTML5 canvas getImageData work on external images 
* without throwing the same origin policy error.
* 
* Copyright (C) 2012  Adamantium Solutions (www.adamantium.sk)
* 
* @package     jquery.crystalGallery
* @author      Adamantium Solutions
* @copyright   2012 Adamantium Solutions
* @link        http://www.adamantium.sk
*/



// Get external image
try {
    /**
    * GetExternalImageData class
    * 
    * @param string $url            URL of external image to load
    * @param boolean $useCache      use external image caching
    */
    new GetExternalImageData($_GET["url"], $_GET["cache"]);
    
// Or throw an error
} catch (Exception $e) {    
    $getExternalImageData = new GetExternalImageData();
    $getExternalImageData->generateErrorImage($e);
}
  


/**
* Class GetExternalImageData
* 
* get an image from external link and return it as local image
* 
*/
class GetExternalImageData {
    
    private $allowedExtensions = array("jpg", "jpeg", "png", "gif");    //valid external image extensions
    
    private $errorImageWidth = 1280;                                    //width of error reporting image
    private $errorImageHeight = 800;                                    //height of error reporting image
    
    private $useCache = false;                                          //use external image caching   
    private $cacheDir = "cache";                                        //caching folder directory
        
    /**
    * System variables
    */
    private $imageUrl, 
            $hashedFilename,
            $imageExtension,
            $root,
            $webRoot = "";
     
    /**
    * Class constructor
    * 
    * @param string $url            URL of external image to load
    * @param boolean $useCache      use external image caching
    */
    function __construct($url = false, $useCache = false) {
        
        if($url !== false) {
            
            // Decode the URL
            if($this->isValidURL($url)) {
                
                $this->imageUrl = urldecode($url);   
                $this->useCache = $useCache;   
                
                // Get file extension
                $extension = pathinfo($this->imageUrl);
                $this->imageExtension = strtolower($extension["extension"]);
                
                $this->hashedFilename = sha1($this->imageUrl) . ".{$this->imageExtension}";
        
                // Get root directories
                if(!isset($_SERVER['PATH_TRANSLATED'])) $_SERVER['PATH_TRANSLATED'] = $_SERVER['SCRIPT_FILENAME'];
                $this->root = $_SERVER["PATH_TRANSLATED"] ? dirname($_SERVER["PATH_TRANSLATED"]) : dirname($_SERVER["SCRIPT_FILENAME"]);
                $this->webRoot = (in_array(dirname($_SERVER["SCRIPT_NAME"]), array("/", "\\"))) ? "" : dirname($_SERVER["SCRIPT_NAME"]);
                
                // Get image from cache
                if($this->useCache) {
                    $this->getImageFromCache();    
                    
                // Get image from URL
                } else {
                    $this->getImageFromURL();    
                }
            
            // Show error
            } else {
                $this->generateErrorImage("No valid URL was specified!");    
            }                
        }
        
        return;
    }   
    
    /**
    * Get image from url with the correct method 
    * cURL or file_get_contents
    * 
    */
    private function getImageFromURL() {
        
        // Validate image extension
        if(!$this->isValidImage()) {
            $this->generateErrorImage("Invalid image extension: {$this->imageUrl}", "Allowed types are (" . implode(", ", $this->allowedExtensions) . ").");
            return;            
        }
        
        // Check for curl of file_get_contents
        $allowUrlFopen = ini_get("allow_url_fopen");
        $curlEnabled = function_exists('curl_init');
        
        // Get image by cURL
        if($curlEnabled) {
            $data = $this->getImageContentsCurl();
            
        // Get image by file_get_tontents
        } else if ($allowUrlFopen) {
            $data = $this->getImageContents();
            
        // Show error    
        } else {
            $this->generateErrorImage("cURL or allow_url_fopen must be enabled on this server to get external images!");           
            return;
        }
        
        $this->outputAsLocalImage($data);    
        
        return;
    }
    
    /**
    * Return image from cache if exists
    * 
    */
    private function getImageFromCache() {
        
        // get from cache
        if(file_exists("{$this->root}/{$this->cacheDir}/{$this->hashedFilename}")) {
            header("Location: {$this->webRoot}/{$this->cacheDir}/{$this->hashedFilename}");
            exit();
            
        //get from external URL
        } else {
            $this->getImageFromURL();    
        }
        
        return;
    }
    
    /**
    * Get url with file_get_contents
    * 
    */
    private function getImageContents() {
        return file_get_contents($this->imageUrl);
    }
    
    /**
    * Get url with cUrl
    * 
    */
    private function getImageContentsCurl() {
        
        $url = $this->imageUrl;
         
        $timeout = 5;
        $channel = curl_init();
        curl_setopt($channel, CURLOPT_URL, $url);
        curl_setopt($channel, CURLOPT_RETURNTRANSFER, 1);
        curl_setopt($channel, CURLOPT_CONNECTTIMEOUT, $timeout);
        $image = curl_exec($channel);
        curl_close($channel);
        
        return $image; 
    }
    
    /**
    * Output image to browser with correct image type
    * 
    * @param string $data
    */
    private function outputAsLocalImage($data) {
        
        // Check if it is an image
        if($image = @imagecreatefromstring($data)) {
        
            // Get image mime type
            $pathinfo = pathinfo($this->imageUrl);
            $type = $pathinfo["extension"];
            
            $mimeTypes = array(
                "jpg" => "image/jpeg",
                "jpeg" => "image/jpeg",
                "png" => "image/png",
                "gif" => "image/gif",
            );
            $type = $mimeTypes[strtolower($type)];
            
            //save image to cache folder
            if($this->useCache) {
                $dir = "{$this->root}/{$this->cacheDir}";
                //make dir if it does not exists
                if(!is_dir($dir)) mkdir($dir, 0775, true);
                
                //check if dir is writable
                if($this->isWritable($dir)) $cacheFile = "$dir/{$this->hashedFilename}";    
            }
             
            header("Content-type: $type");
            
            // Output jpeg image
            if($type == "image/jpeg") {
                imagejpeg($image, $cacheFile);    
                
            // Output png image
            } else if ($type == "image/png") {
                imagealphablending($image, false);
                imagesavealpha($image, true);  
                imagepng($image, $cacheFile);
            
            // Output gif image
            } else if ($type == "image/gif") {
                imagegif($image, $cacheFile);
            }         
            
            //get image from cache
            if($cacheFile) $this->getImageFromCache();
            
            imagedestroy($image);   
            
        // The requested file is not an image
        } else {
            $this->generateErrorImage("Invalid image specified: {$this->imageUrl}");
        }   
        
        return;
    }
    
    /**
    * Check for valid extension
    * 
    */
    private function isValidImage() {
        return in_array($this->imageExtension, $this->allowedExtensions);
    }
    
    /**
    * Check valid URL
    * 
    * @param string $url
    */
    private function isValidURL($url) {
        return preg_match('|^http(s)?://[a-z0-9-]+(.[a-z0-9-]+)*(:[0-9]+)?(/.*)?$|i', $url);
    } 
    
    /**
    * Check if folder is writable
    * 
    * @param mixed $url
    */
    private function isWritable($url) {
        if(is_writable($url)) {
            return true;    
        } else {
            $this->generateErrorImage("Cache dir is not writable: $url");
            exit();
        }          
    }       
    
    /**
    * Generate error image with given string
    * 
    * @param string $text
    */
    public function generateErrorImage($text, $text2 = "") {
        
        // Create image
        $im = imagecreatetruecolor($this->errorImageWidth, $this->errorImageHeight);

        // Create colors
        $red = imagecolorallocate($im, 220, 70, 0);  // #dc4600
        $gray = imagecolorallocate($im, 70, 70, 70); // #464646
        
        // Fill the image with background
        imagefilledrectangle($im, 0, 0, $this->errorImageWidth, $this->errorImageHeight, $gray);

        // Error title
        $errorText = "An error occurred!";
        $position = $this->getTextCenter($errorText, 5);
        imagestring($im, 5, $position["horizontal"], $position["vertical"] - 40, $errorText, $red); 
        
        // Error text
        $position = $this->getTextCenter($text, 5);
        imagestring($im, 5, $position["horizontal"], $position["vertical"], $text, $red); 
        
        if($text2) {
            // New line error text
            $position = $this->getTextCenter($text2, 5);
            imagestring($im, 5, $position["horizontal"], $position["vertical"] + 20, $text2, $red); 
        }
        
        // Using imagepng() results in clearer text compared with imagejpeg()
        header('Content-Type: image/png');
        imagepng($im);
        imagedestroy($im);
        
        return;
    }
    
    /**
    * Get center position on image for font size and text
    * 
    * @param string $text
    * @param integer $size
    */
    private function getTextCenter($text, $size) {
        
        $font_width = imagefontwidth($size);
        $font_height = imagefontheight($size);
        
        // Horizontal center position
        $text_width = $font_width * strlen($text);
        $position_horizontal = ceil(($this->errorImageWidth - $text_width) / 2);

        // Vertical center position
        $text_height = $font_height;
        $position_vertical = ceil(($this->errorImageHeight - $text_height) / 2);
        
        $position = array(
            "horizontal" => $position_horizontal,
            "vertical" => $position_vertical
        );
        
        return $position;
    }
}    