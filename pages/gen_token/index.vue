<template>
<div>
    <h3 class="text-center title-web my-4">GENERATE TOKEN</h3>
    <!-- <img class="logo-content" src="https://img.lovepik.com/photo/50047/9025.jpg_wh860.jpg" alt=""> -->
    <div class="container">
        <form @submit.prevent="submitToken">
            <div class="length-prefix">
                <div class="form-group form-length">
                    <label class="form-length-title " for="">Length</label>
                    <select v-model="length" class="form-select" name="" id="">
                        <option value="64">64-bit - Low security tokens</option>
                        <option value="128">128-bit - Medium security tokens</option>
                        <option value="256">256-bit - Authorization tokens</option>
                        <option value="512">512-bit - Refresh tokens</option>
                        <option value="1024">1024-bit - Secured access tokens</option>
                        <option value="2048">2048-bit - High security access tokens</option>
                    </select>
                </div>
                <div class="form-group form-prefix">
                    <label class="form-prefix-title" for="">Prefix</label>
                    <input v-model="prefix" type="text" class="form-control">
                </div>
            </div>

            <div class="setting">
                <div class="settingLeft">
                    <div class="form-group">
                        <label class="content-label">Uppercase</label>
                        <div class="toggle-switch">
                            <input type="checkbox" id="uppercaseToggle" v-model="includeUppercase">
                            <label for="uppercaseToggle" class="switch"></label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="content-label">Lowercase</label>
                        <div class="toggle-switch">
                            <input type="checkbox" id="lowercaseToggle" v-model="includeLowercase">
                            <label for="lowercaseToggle" class="switch"></label>
                        </div>
                    </div>
                </div>
                <div class="settingRight">
                    <div class="form-group">
                        <label class="content-label">Numbers</label>
                        <div class="toggle-switch">
                            <input type="checkbox" id="numbersToggle" v-model="includeNumbers">
                            <label for="numbersToggle" class="switch"></label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="content-label">SpecialChars</label>
                        <div class="toggle-switch">
                            <input type="checkbox" id="characterToggle" v-model="includeCharacter">
                            <label for="characterToggle" class="switch"></label>
                        </div>
                    </div>
                </div>
            </div>

            <input type="submit" value="Generate" class="btn btn-primary">
        </form>
        <div class="form-group my-3">
            <label for="">
                Your token
            </label>
            <!-- <input class="form-control" type="text" > -->
            <textarea placeholder="This is your token" v-model="token" class="form-control place-token" name="" id=""></textarea>
            <button v-if="isRender" type="button" class="btn btn-primary my-4" @click="copyText()">Copy</button>
            <div>
                <span v-if="isCopy">Copied</span>
            </div>
        </div>
        <!-- <div class="form-group hash">
            <div class="hash-title-dad my-6">
                <h3 class="text-center">
                    API Tokens with Base 64, MD5 Hash, Sha256 and Sha512
                </h3>
            </div>
            <div class="hash-content-dad">
                <hr>
                <div class="base64 hash-content-dad-item">
                    <div class="base64-title hash-title">
                        Base64
                    </div>
                    <div class="base64-content hash-content">

                        {{ base64 }}

                    </div>
                </div>
                <hr>
                <div class="hash-content-dad-item">
                    <div class="hash-title">
                        MD5 Hash
                    </div>
                    <div class="hash-content">

                        {{ md5Hash }}

                    </div>
                </div>
                <hr>
                <div class="hash-content-dad-item">
                    <div class="hash-title">
                        Sha256
                    </div>
                    <div class="hash-content">

                        {{ sha256 }}

                    </div>
                </div>
                <hr>
                <div class="hash-content-dad-item">
                    <div class="hash-title">
                        Sha512
                    </div>
                    <div class=" hash-content">

                        {{ sha512 }}

                    </div>
                </div>
                <hr>
            </div>

        </div> -->
        <token-hash :base64="base64" :md5Hash="md5Hash" :sha256="sha256" :sha512="sha512"/>
        <div class="the-end mt-5">
            <div class="the-end-top">
                <h6 class="text-center">
                     If you expect more from this tool, contact me : thang14012003@gmail.com.
                     We are based in France, and we specialize in randomizing stuff. We'll be happy to help.
                </h6>
                 
            </div>
          
            <div class="the-end-bottom">
               <span class="text-center d-block">
                Generate-random.org uses no sort of Database, because it would take us in some complicated privacy issues that we are happy to stay away from, and also because it would be expensive to host for the environment and for our wallet.
                You can be sure that what you was generated by our server is only between you and your browser.
               </span>
            </div>
        </div>
        <form-message v-if="isNotLength" :content="`Length can not be null`" @cancel="closeModal" />
        <form-message v-if="isAllFalse" :content="`At least 1 option needs to be enabled`" @cancel="closeModal" />
        <form-message v-if="isFalseServer" :content="`Server error`" @cancel="closeModal" />

    </div>
    <default-footer />
</div>
</template>

<script>
import DefaultFooter from '../../components/Footer/DefaultFooter.vue';
import FormMessage from '../../components/FormMesage/FormMessage.vue';
import TokenHash from '../../components/TokenHash/TokenHash.vue';
export default {
    head: {
        title: 'IT TOOLS | GEN TOKEN'
    },
    components: {
        FormMessage,
        DefaultFooter,
        TokenHash
    },
    data() {
        return {
            includeUppercase: false,
            includeLowercase: false,
            includeNumbers: false,
            includeCharacter: false,
            isNotLength: false,
            isAllFalse: false,
            isFalseServer: false,
            isRender: false,
            isCopy: false,
            length: null,
            prefix: '',
            token: '',
        }
    },
    methods: {
        closeModal() {
            this.isNotLength = false;
            this.isAllFalse = false;
            this.isFalseServer = false;
        },
        async copyText() {
            await navigator.clipboard.writeText(this.token);
            this.isCopy = true;
            // setTimeout(() => {
            //     this.isCopy = false;
            // }, 3000)
        },
        async submitToken() {
            this.isCopy = false;
            console.log("this.length : ", this.length);
            if (!this.includeCharacter && !this.includeLowercase && !this.includeUppercase && !this.includeNumbers) {
                this.isAllFalse = true;
                return;
            }
            if (this.length == null || this.length == '') {
                this.isNotLength = true;
                return;
            }
            try {
                const res = await this.$axios.$post(`${process.env.baseApiUrlToken}/generate`, {
                    "includeUppercase": this.includeUppercase,
                    "includeLowercase": this.includeLowercase,
                    "includeNumbers": this.includeNumbers,
                    "includeSpecialChars": this.includeCharacter,
                    // "length": parseInt(this.length),
                    "length": parseInt(this.length),
                    "prefix": this.prefix,
                    // includeUppercase: this.includeUppercase,
                    // includeLowercase: this.includeLowercase,
                    // includeNumbers: this.includeNumbers,
                    // includeSpecialChars: this.includeCharacter,
                    // length: parseInt(this.length)

                    // cả 2 đều được
                })
                console.log("type of res", typeof (res));
                this.token = this.prefix + res.token[0];
                this.base64 = 'base64:'+res.token[1] ;
                this.sha256 = res.token[3];
                this.md5Hash = res.token[2];
                this.sha512 = res.token[4];
                console.log("type of token", typeof (this.token));
                console.log(this.includeUppercase);
                console.log(this.includeLowercase);
                console.log(this.includeNumbers);
                console.log(this.includeCharacter);
                console.log('length:', typeof (this.length));
                console.log("token:", this.token[1]);
                this.isRender = true;
            } catch (ex) {
                this.isFalseServer = true;
            }

        },
        async submitTokenv1() {
            try {
                const res = await this.$axios.$get(
                    `https://localhost:7146/api/Token/generatev1?includeUppercase=${this.includeUppercase}&includeLowercase=${this.includeLowercase}&includeNumbers=${this.includeNumbers}&includeSpecialChars=${this.includeCharacter}`, {
                        includeNumbers: this.includeNumbers
                        // đoạn numbers này k cần truyền cũng được vì nó lấy fix cứng ở api
                    })
                this.token = res.token;
                console.log(this.includeUppercase);
                console.log(this.includeLowercase);
                console.log(this.includeNumbers);
                console.log(this.includeCharacter);
                console.log(typeof (this.length));
                console.log(this.token);
            } catch (ex) {
                this.isAllFalse = true;
            }
        },
        // truyền vào params như dưới này thì mới nhận được length, nhưng mà người ta thường k dùng cách này ở bên phía api
        // async submitTokenv1() {
        //     try {
        //         const res = await this.$axios.$get(`https://localhost:7146/api/Token/generatev1`, {
        //             params: {
        //                 length: this.length,
        //                 includeUppercase: this.includeUppercase,
        //                 includeLowercase: this.includeLowercase,
        //                 includeNumbers: this.includeNumbers,
        //                 includeSpecialChars: this.includeCharacter
        //             }
        //         });

        //         this.token = res.token;
        //         console.log(this.includeUppercase);
        //         console.log(this.includeLowercase);
        //         console.log(this.includeNumbers);
        //         console.log(this.includeCharacter);
        //         console.log(typeof (this.length));
        //         console.log(this.token);
        //     } catch (ex) {
        //         console.log("Có lỗi");
        //     }
        // }
    }
}
</script>

<style scoped>
.container {
    padding: 20px 60px 60px;
    width: 800px;
    background-color: #f3f7fa;
    border-radius: 6px;
    box-shadow: 0 0 4px 0px grey;
}

.setting {
    display: flex;
    justify-content: space-around;
    padding: 30px;
}

.logo-content {
    height: 100px;
    width: auto;
    display: flex;
    margin: auto;
}

.length-prefix {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
}

.toggle-switch {
    position: relative;
    display: inline-block;
    width: 60px;
    height: 34px;

}

.toggle-switch input {
    opacity: 1;
    width: 0;
    height: 0;
}
.the-end-top {

}
.switch {
    border-radius: 1px solid grey;
    position: absolute;
    cursor: pointer;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: #ccc;
    transition: .4s;
    border-radius: 34px;
}

.switch:before {
    position: absolute;
    content: "";
    height: 24px;
    width: 24px;
    left: 0px;
    bottom: 1px;
    background-color: white;
    transition: .4s;
    border-radius: 50%;
}

input:checked+.switch {
    background-color: #2196F3;
}

input:checked+.switch:before {
    transform: translateX(36px);
}

.content-label {
    min-width: 100px;
}

.form-length-title {
    margin: 0;
    padding: 7px;
    border-top-left-radius: 6px;
    border-bottom-left-radius: 6px;
    background-color: rgb(220, 220, 220)
}

.form-prefix-title {
    margin: 0;
    padding: 7px;
    border-top-left-radius: 6px;
    border-bottom-left-radius: 6px;
    background-color: rgb(220, 220, 220)
}

.form-length,
.form-prefix {
    /* width: px; */
    display: flex;
    border-radius: 6px;
    flex-direction: row;
}

.form-select {
    border-top-right-radius: 6px;
    border-bottom-right-radius: 6px;
    outline: none;
}

.form-prefix {
    display: flex;
    flex-direction: row;
}

.hash-title-dad {}


.place-token {
    color: #0a36ff;
}
</style>

// Khi trang web tải, checkbox <input type="checkbox"> được ẩn đi nhờ thuộc tính opacity: 0 trong CSS.
// label với lớp switch sẽ hiển thị như một công tắc bật/tắt nhờ các thuộc tính CSS đã được định nghĩa.
// Khi người dùng nhấp vào label (do nó được liên kết với checkbox thông qua thuộc tính for), checkbox sẽ thay đổi trạng thái (checked/unchecked).
// Nếu checkbox được chọn (checked), CSS sẽ thay đổi màu nền của công tắc bật/tắt và dịch chuyển phần hình tròn nhỏ bên trong.
// v-model="includeUppercase" sẽ liên kết giá trị của checkbox với biến includeUppercase trong Vue component, giúp bạn có thể theo dõi và sử dụng giá trị này trong logic của ứng dụng.
