class CreateJournalIssues < ActiveRecord::Migration
  def self.up   
    create_table :journal_issues, :force => true do |t|
      t.text     :title
      t.integer  :journal_id,                            :null => false
      t.text     :volume
      t.text     :number
      t.text     :volume_supplement
      t.text     :number_supplement
      t.integer  :year,                                  :null => false
      t.boolean  :incomplete,         :default => false
      t.timestamps
    end
    
    add_index :journal_issues, [:journal_id, :volume, :number, :volume_supplement,:year], :name => "journal_issues_journal_id_key", :unique => true
    add_index :journal_issues, [:journal_id, :volume, :number, :number_supplement,:year], :name => "journal_issues_journal_id_key1", :unique => true
    
    
  end

  def self.down
    remove_index :journal_issues, :name => :journal_issues_journal_id_key
    remove_index :journal_issues, :name => :journal_issues_journal_id_key1
    drop_table :journal_issues
  end
end
