// a classic trie implementation which consumes strings

// trie node class
class TrieNode {
  char: string;
  children: { [key: string]: TrieNode };
  is_end: boolean;
  constructor(char: string) {
    this.char = char;
    this.children = {};
    this.is_end = false;
  }
}

export class Trie {
  root: TrieNode;
  constructor() {
    this.root = new TrieNode("");
  }

  insert(word: string) {
    let curr: TrieNode = this.root;
    for (let i = 0; i < word.length; i++) {
      let char = word.charAt(i);
      if (!curr.children[char]) {
        curr.children[char] = new TrieNode(char);
      }
      curr = curr.children[char];
    }
    curr.is_end = true;
  }

  search(word: string): boolean {
    let curr: TrieNode = this.root;
    for (let i = 0; i < word.length; i++) {
      let char = word.charAt(i);
      if (!curr.children[char]) {
        return false;
      }
      curr = curr.children[char];
    }
    return curr.is_end;
  }

  startsWith(prefix: string): boolean {
    let curr: TrieNode = this.root;
    for (let i = 0; i < prefix.length; i++) {
      let char = prefix.charAt(i);
      if (!curr.children[char]) {
        return false;
      }
      curr = curr.children[char];
    }
    return true;
  }

  // returns k words in the trie that start with the given prefix
  // if there are less than k words all words are returned
  // if no words are found, returns an empty array
  getKWords(prefix: string, k: number): string[] {
    let curr: TrieNode = this.root;
    for (let i = 0; i < prefix.length; i++) {
      let char = prefix.charAt(i);
      if (!curr.children[char]) {
        return [];
      }
      curr = curr.children[char];
    }
    return this.getWordsHelper(curr, prefix, k);
  }

  // helper function for getWords
  getWordsHelper(node: TrieNode, prefix: string, k: number): string[] {
    let words: string[] = [];
    if (node.is_end) {
      words.push(prefix);
    }
    for (let child in node.children) {
      if (words.length >= k) {
        break;
      }
      words = words.concat(this.getWordsHelper(node.children[child], prefix + child, k-words.length));
    }
    return words;
  }
}