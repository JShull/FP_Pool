# Fuzzphyte Unity Tools

## Pool

FP_Pool is designed and built to provide a rather generic solution for object pooling. It requires that you generate a MonoBehaviour class that does the work while you then point to a special component derived c# script 'FP_ObjectPool.cs' class.

## Setup & Design

FP_Pool is designed around the idea that you have some set of base objects that you want to pool. These objects are being requested from the pool and then depending upon their conditions and what you're doing you should release them back to the pool when you're done. This object pool system defaults to keep account of items that are active and the original list - it will grow in size to meet your demands if all objects are active and there's none left in the pool **BUT** it will also clean up after itself and destroy those items once a certain amount of time has passed. Be aware of this built-in logic! It also has an upper max limit on active objects - this system never increases the original pool size it only adds to a queue and eventually all of those items are destroyed/removed. This upper limit is set just to make sure you have a hold on the memory leak.

### Software Architecture

There is currently two parts to the FP_Pool tool.

* 'FP_ObjectPool.cs' which is a c# class derived from the UnityEngine.Component.
  * This class manages everything related to the object pool and you should have a MonoBehaviour object reference this so you can do things in the Unity Editor.
  * Please see the simple URPExample included in the samples of this package to see how to do that.
* 'IFPPoolable.cs' is an interface that we suggest you implement your own MonoBehaviour component class on the root transform of your prefab.
  * This interface isn't required but it is helpful for when the object pool system is activating, releasing, and on first activation you can be notified of said events.
  * Please see the simple URPExample included in the samples of this package to see how we utilize the interface to deactivate the bullet prefab after a few seconds as well as set a max active count.

## Dependencies

Please see the [package.json](./package.json) file for more information.

## License Notes

* This software running a dual license
* Most of the work this repository holds is driven by the development process from the team over at Unity3D :heart: to their never ending work on providing fantastic documentation and tutorials that have allowed this to be born into the world.
* I personally feel that software and it's practices should be out in the public domain as often as possible, I also strongly feel that the capitalization of people's free contribution shouldn't be taken advantage of.
  * If you want to use this software to generate a profit for you/business I feel that you should equally 'pay up' and in that theory I support strong copyleft licenses.
  * If you feel that you cannot adhere to the GPLv3 as a business/profit please reach out to me directly as I am willing to listen to your needs and there are other options in how licenses can be drafted for specific use cases, be warned: you probably won't like them :rocket:

### Educational and Research Use MIT Creative Commons

* If you are using this at a Non-Profit and/or are you yourself an educator and want to use this for your classes and for all student use please adhere to the MIT Creative Commons License
* If you are using this back at a research institution for personal research and/or funded research please adhere to the MIT Creative Commons License
  * If the funding line is affiliated with an [SBIR](https://www.sbir.gov) be aware that when/if you transfer this work to a small business that work will have to be moved under the secondary license as mentioned below.

### Commercial and Business Use GPLv3 License

* For commercial/business use please adhere by the GPLv3 License
* Even if you are giving the product away and there is no financial exchange you still must adhere to the GPLv3 License

## Contact

* [John Shull](mailto:the.john.shull@gmail.com)
* [Twitter](https://twitter.com/TheJohnnyFuzz)