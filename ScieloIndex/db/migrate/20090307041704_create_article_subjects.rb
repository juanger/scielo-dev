class CreateArticleSubjects < ActiveRecord::Migration
  def self.up
    create_table :article_subjects, :force => true do |t|
      t.integer  :article_id,  :null => false
      t.integer  :subject_id,  :null => false
      t.timestamps
    end
    
    add_index :article_subjects, [:article_id, :subject_id], :name => "article_subjects_article_id_key", :unique => true
    
  end

  def self.down
    remove_index :article_subjects, :name => :article_subjects_article_id_key
    drop_table :article_subjects
  end
end
