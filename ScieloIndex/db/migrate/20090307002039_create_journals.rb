class CreateJournals < ActiveRecord::Migration
  def self.up
    create_table :journals, :force => true do |t|
      t.text     :title,                           :null => false
      t.integer  :country_id,                      :null => false
      t.text     :state
      t.text     :city
      t.integer  :publisher_id,                    :null => false
      t.text     :url
      t.text     :email
      t.text     :other
      t.boolean  :incomplete,   :default => false
      t.text     :abbrev
      t.text     :issn
      t.timestamps
    end

    add_index :journals, [:issn], :name => "journals_issn_key", :unique => true
  end

  def self.down
    remove_index :journals, :name => :journals_issn_key

    drop_table :journals
  end
end